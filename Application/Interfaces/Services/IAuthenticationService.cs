
using Sarafi.Application.Applications.Users.Dtos;

namespace Sarafi.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> AuthenticateAsync(LoginDto loginDto);
    }
}
