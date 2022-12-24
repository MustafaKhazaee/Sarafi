using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sarafi.Application.Applications.MasterAccounts.Commands;
using Sarafi.Application.Applications.MasterAccounts.Queries;
using Sarafi.Domain.Entities;

namespace Sarafi.API.Controllers
{
    public class MasterAccountController : SarafiV1Controller
    {
        public MasterAccountController(IMediator mediator) : base(mediator) { }

        [HttpGet(nameof(GetAllMasterAccountsAsync))]
        public async Task<List<MasterAccount>> GetAllMasterAccountsAsync () =>
            await mediator.Send(new GetAllMasterAccountsQuery());

        [HttpPost(nameof(AddMasterAccountAsync))]
        public async Task<string> AddMasterAccountAsync(AddMasterAccountCommand command) =>
            await mediator.Send(command);
    }
}
