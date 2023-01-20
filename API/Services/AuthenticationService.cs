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
    public async Task<LoginResponse> AuthenticateAsync(LoginDto loginDto)
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
            string hashedPassword = $"{user.Password}{user.Salt}".GetHash();
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
                        string token = await GenerateJWT(user, roles, permissions);
                        response.Response = AuthenticationMessages.UserAuthorized;
                        response.Token = token;
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
    private async Task<string> GenerateJWT(User user, List<string> roles, List<string> permissions)
    {
        SymmetricSecurityKey key = new(Encoding.Unicode.GetBytes(_issuerSigninKey));
        string algorithm = SecurityAlgorithms.HmacSha512;
        SigningCredentials credentials = await Task.Run(() => new SigningCredentials(key, algorithm));
        List<Claim> claims = await Task.Run(() => GetClaims(user, roles, permissions));
        JwtSecurityToken securityToken = await Task.Run(() => new JwtSecurityToken(_validIssuer, _validAudience, claims, null, user.ExpirationDate, credentials));
        JwtSecurityTokenHandler handler = new();
        return await Task.Run(() => handler.WriteToken(securityToken));
    }
    private static List<Claim> GetClaims(User user, List<string> roles, List<string> permissions)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", $"{user.Id}"),
            new Claim("Name", $"{user}"),
            new Claim("CompanyId", $"{user.Company.Id}")
        };
        permissions.ForEach(p => claims.Add(new Claim(ClaimTypes.Role, p))); // for authorization
        roles.ForEach(r => claims.Add(new Claim("Role", r)));  // For نقش
        return claims;
    }
}
