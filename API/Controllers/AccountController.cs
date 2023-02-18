using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sarafi.Application.Applications.Accounts.Commands;
using Sarafi.Application.Applications.Accounts.Dtos;
using Sarafi.Application.Applications.Accounts.Queries;
using Sarafi.Domain.Constants;

namespace Sarafi.API.Controllers
{
    public class AccountController : SarafiV1Controller
    {
        public AccountController(IMediator mediator) : base(mediator) { }

        //[HttpPost(nameof(AddAccountAsync)), Authorize(Roles = AuthorizationRoles.Accounts.Post)]
        [HttpPost(nameof(AddAccountAsync))]
        public async Task<string> AddAccountAsync(AddAccountCommand addAccountCommand) => 
            await mediator.Send(addAccountCommand);
        
        [HttpGet(nameof(GetAllAccountsAsync)), Authorize(Roles = AuthorizationRoles.Accounts.Get)]
        public async Task<List<AccountDto>> GetAllAccountsAsync() =>
            await mediator.Send(new GetAllAccountsQuery());
    }
}   
