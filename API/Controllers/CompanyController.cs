using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sarafi.Application.Applications.Companies.Commands;
using Sarafi.Application.Applications.Companies.Queries;
using Sarafi.Domain.Entities;

namespace Sarafi.API.Controllers
{
    public class CompanyController : SarafiV1Controller
    {
        public CompanyController(IMediator mediator) : base(mediator) { }

        [HttpGet(nameof(GetAllCompaniesQuery))]
        public async Task<List<Company>> GetAllCompaniesAsync() =>
            await mediator.Send(new GetAllCompaniesQuery());

        [HttpPost(nameof(AddCompanyAsync))]
        public async Task<string> AddCompanyAsync(AddCompanyCommand companyCommand) =>
            await mediator.Send(companyCommand);
    }
}
