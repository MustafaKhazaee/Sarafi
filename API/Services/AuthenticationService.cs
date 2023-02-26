using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Application.Applications.Users.Dtos;
using Sarafi.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Sarafi.Application.Extensions;
using Sarafi.Domain.Entities;
using System.Security.Claims;
using System.Text;
using Sarafi.Domain.Constants;

namespace Sarafi.API.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly string _issuerSigninKey;
    private readonly string _validAudience;
    private readonly string _validIssuer;
    private readonly IUnitOfWork uow;

    public AuthenticationService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _issuerSigninKey = configuration["Jwt:IssuerSigninKey"];
        _validAudience = configuration["Jwt:ValidAudience"];
        _validIssuer = configuration["Jwt:ValidIssuer"];
        uow = unitOfWork;
    }

    public async Task<LoginResponse> AuthenticateAsync(LoginRequest loginDto)
    {
        LoginResponse response = new LoginResponse();
        User? user = await uow.UserRepository.GetUserByUsernameAsync(loginDto.UserName);
        if (user == null)
        {
            response.Response = AuthenticationMessages.UserNotFound;
        }
        else if (user.IsLocked)
        {
            response.Response = AuthenticationMessages.UserIsLocked;
        }
        else
        {
            string hashedPassword = $"{loginDto.Password}{user.Salt}".GetHash();
            if (user.Password.Equals(hashedPassword))
            {
                List<Role> Roles = new();
                List<string> roles = user.UserRoles.Select(ur =>
                {
                    Roles.Add(ur.Role);
                    return ur.Role.RoleName;
                }).ToList();
                if (roles.IsNullOrEmpty())
                {
                    response.Response = AuthenticationMessages.UserHasNoRoles;
                }
                else
                {
                    List<string> permissions = Roles.SelectMany(r => r.RolePermissions).Select(rp => rp.Permission.PermissionCode).ToList<string>();
                    if (permissions.IsNullOrEmpty())
                    {
                        response.Response = AuthenticationMessages.UserHasNoPermissions;
                    }
                    else
                    {
                        //DateTime refDate = user.ExpirationDate;

                        DateTime refDate = DateTime.Now.AddMinutes(3);
                        DateTime dateTime = DateTime.Now.AddMinutes(Values.RefreshTokenLifeTimeMinutes);
                        string refreshToken = await GenerateJWT(user, roles, permissions, refDate);
                        string accessToken = await GenerateJWT(user, roles, permissions, dateTime);
                        await uow.UserRepository.SetRefreshTokenAsync(user.Id, refreshToken.GetHash());
                        response.Response = AuthenticationMessages.UserAuthorized;
                        response.RefreshToken = refreshToken;
                        response.AccessToken = accessToken;
                    }
                }
            }
            else
            {
                response.Response = AuthenticationMessages.WrongPassword;
            }
        }
        return response;
    }

    public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        if (refreshTokenRequest == null || refreshTokenRequest.RefreshToken == null || refreshTokenRequest.UserId == null)
        {
            return new RefreshTokenResponse { IsRefreshed = false };
        }
        User? user = await uow.UserRepository.GetUserByIdAsync(refreshTokenRequest.UserId.Value);
        if (user == null || user.RefreshToken == null) return new RefreshTokenResponse { IsRefreshed = false };
        var isValid = IsTokenValid(refreshTokenRequest, user.RefreshToken, user.Id);
        if (!isValid)
        {
            return new RefreshTokenResponse { IsRefreshed = false };
        }
        List<Role> Roles = new();
        List<string> roles = user.UserRoles.Select(ur =>
        {
            Roles.Add(ur.Role);
            return ur.Role.RoleName;
        }).ToList();
        List<string> permissions = Roles.SelectMany(r => r.RolePermissions).Select(rp => rp.Permission.PermissionCode).ToList<string>();
        DateTime dateTime = DateTime.Now.AddMinutes(Values.RefreshTokenLifeTimeMinutes);
        var accessToken = await GenerateJWT(user, roles, permissions, dateTime);
        return new RefreshTokenResponse { IsRefreshed = true, AccessToken = accessToken };
    }

    private async Task<string> GenerateJWT(User user, List<string> roles, List<string> permissions, DateTime expirationDate)
    {
        SymmetricSecurityKey key = new(Encoding.Unicode.GetBytes(_issuerSigninKey));
        string algorithm = SecurityAlgorithms.HmacSha512;
        SigningCredentials credentials = await Task.FromResult(new SigningCredentials(key, algorithm));
        List<Claim> claims = await Task.FromResult(GetClaims(user, roles, permissions));
        JwtSecurityToken securityToken = await Task.FromResult(new JwtSecurityToken(
            _validIssuer, _validAudience, claims, null, expirationDate, credentials
        ));
        JwtSecurityTokenHandler handler = new();
        return await Task.FromResult(handler.WriteToken(securityToken));
    }

    private static List<Claim> GetClaims(User user, List<string> roles, List<string> permissions)
    {
        List<Claim> claims = new()
        {
            new Claim(UserClaims.Id, $"{user.Id}"),
            new Claim(UserClaims.Name, $"{user}"),
            new Claim(UserClaims.CompanyId, $"{user.CompanyId}")
        };
        permissions.ForEach(p => claims.Add(new Claim(ClaimTypes.Role, p))); // for authorization
        roles.ForEach(r => claims.Add(new Claim(UserClaims.Role, r)));  // For نقش
        return claims;
    }

    private bool IsTokenValid (RefreshTokenRequest refreshTokenRequest, string refreshTokenDb, long userIdDb)
    {
        var token = refreshTokenRequest.RefreshToken;
        var userId = refreshTokenRequest.UserId.Value;
        if (!token.GetHash().Equals(refreshTokenDb) || userId != userIdDb) return false;
        var tokenHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey key = new(Encoding.Unicode.GetBytes(_issuerSigninKey));
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidAudience = _validAudience,
                ValidIssuer = _validIssuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var tokenUserId = long.Parse(jwtToken.Claims.First(x => x.Type == UserClaims.Id).Value);
            return userId == tokenUserId;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
