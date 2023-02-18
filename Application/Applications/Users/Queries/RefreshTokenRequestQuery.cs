
using MediatR;
using Sarafi.Application.Applications.Users.Dtos;
using Sarafi.Application.Interfaces.Services;

namespace Sarafi.Application.Applications.Users.Queries
{
    public class RefreshTokenRequestQuery : IRequest<RefreshTokenResponse>
    {
        public long? UserId { set; get; }
        public string RefreshToken { set; get; }
    }
    public class RefreshTokenRequestQueryHandler : IRequestHandler<RefreshTokenRequestQuery, RefreshTokenResponse>
    {
        private readonly IAuthenticationService _authService;
        public RefreshTokenRequestQueryHandler (IAuthenticationService authentication)
        { 
            _authService = authentication;
        }
        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequestQuery request, CancellationToken cancellationToken)
        {
            RefreshTokenRequest refreshTokenRequest = new ()
            {
                RefreshToken = request.RefreshToken, UserId = request.UserId
            };

            var a = await _authService.RefreshToken(refreshTokenRequest);
            return a;
        }
    }
}
