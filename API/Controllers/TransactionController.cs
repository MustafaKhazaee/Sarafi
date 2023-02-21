
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sarafi.Application.Applications.Transactions.Commands;
using Sarafi.Application.Applications.Transactions.Dtos;
using Sarafi.Application.Applications.Transactions.Queries;

namespace Sarafi.API.Controllers
{
    public class TransactionController : SarafiV1Controller
    {
        public TransactionController(IMediator mediator) : base(mediator) { }

        [HttpGet(nameof(GetAllTransactionsAsync))]
        public async Task<List<TransactionDto>> GetAllTransactionsAsync() => await mediator.Send(new GetAllTransactionsQuery());

        [HttpPost(nameof(AddTransactionAsync))]
        public async Task<int> AddTransactionAsync(AddTransactionCommand addTransactionCommand) =>
            await mediator.Send(addTransactionCommand);
    }
}
