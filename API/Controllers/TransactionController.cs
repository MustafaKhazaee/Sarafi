
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sarafi.Application.Applications.Transactions.Commands;
using Sarafi.Application.Applications.Transactions.Dtos;
using Sarafi.Application.Applications.Transactions.Queries;

namespace Sarafi.API.Controllers
{
    public class TransactionController : ControllerBase
    {
        IMediator mediator;
        public TransactionController(IMediator mediator) { this.mediator = mediator; }

        [HttpGet(nameof(GetAllTransactionsAsync))]
        public async Task<List<TransactionDto>> GetAllTransactionsAsync() => await mediator.Send(new GetAllTransactionsQuery());

        [HttpPost(nameof(AddTransactionAsync))]
        public async Task<string> AddTransactionAsync(AddTransactionCommand addTransactionCommand) =>
            await mediator.Send(addTransactionCommand);
    }
}
