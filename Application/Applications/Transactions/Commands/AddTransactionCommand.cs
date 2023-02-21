
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Transactions.Commands
{
    public class AddTransactionCommand : Mappable<AddTransactionCommand, Transaction>, IRequest<int>
    {
        public decimal? Amount { set; get; }
        public long? FromAccountId { set; get; }
        public long? ToAccountId { set; get; }
        public bool? IsApproved { set; get; } = true;
        public DateTime? DateTime { set; get; }
        public TransactionType? TransactionType { set; get; }
        public TransactionStatus? Status { set; get; }
        public string? ToPerson { set; get; }
        public string? Remarks { set; get; }
        public string? SlipPhoto { set; get; }
    }
    public class AddTransactionCommandhandler : IRequestHandler<AddTransactionCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AddTransactionCommandhandler(IUnitOfWork unitOfWork, IValidator<AddTransactionCommand> validator, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<int> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _uow.AccountRepository.FindByIdAsync(request.FromAccountId.Value, cancellationToken);
            var toAccount = await _uow.AccountRepository.FindByIdAsync(request.ToAccountId.Value, cancellationToken);

            if (fromAccount == null || toAccount == null || fromAccount.Balance >= request.Amount.Value)
            {
                return 0;
            }

            fromAccount.DeductBalance(request.Amount.Value);
            toAccount.AddBalance(request.Amount.Value);

            var transaction = _mapper.Map<Transaction>(request);

            await _uow.TransactionRepository.AddAsync(transaction, cancellationToken);
            return await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
