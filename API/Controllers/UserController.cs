using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sarafi.Application.Applications.Users.Dtos;
using Sarafi.Application.Applications.Users.Queries;

namespace Sarafi.API.Controllers
{
    public class UserController : SarafiV1Controller
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost(nameof(AuthenticateAsync)), AllowAnonymous]
        public async Task<LoginResponse> AuthenticateAsync (LoginUserQuery loginUser) => await mediator.Send(loginUser);

        [HttpPost(nameof(RefreshTokenAsync)), AllowAnonymous]
        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequestQuery request) => await mediator.Send(request);
    }
}
