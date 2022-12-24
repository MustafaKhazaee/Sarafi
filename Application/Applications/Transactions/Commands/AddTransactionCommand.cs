
using AutoMapper;
using FluentValidation;
using MediatR;
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Transactions.Commands
{
    public class AddTransactionCommand : Mappable<AddTransactionCommand, Transaction>, IRequest<string>
    {
        public decimal Amount { set; get; }
        public decimal Commission { set; get; }
        public long? FromAccountId { set; get; }
        public long? ToAccountId { set; get; }
        public bool IsApproved { set; get; } = true;
        public DateTime DateTime { set; get; }
        public TransactionType TransactionType { set; get; }
        public TransactionStatus Status { set; get; }
        public string? ToPerson { set; get; }
        public string? Remarks { set; get; }
        public string? SlipPhoto { set; get; }
    }
    public class AddTransactionCommandhandler : IRequestHandler<AddTransactionCommand, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<AddTransactionCommand> _validator;
        private readonly IMapper _mapper;
        public AddTransactionCommandhandler(IUnitOfWork unitOfWork, IValidator<AddTransactionCommand> validator, IMapper mapper)
        {
            _uow = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }
    
        public async Task<string> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            return $"{(validation.IsValid ? await _uow.TransactionRepository.AddAsync(_mapper.Map<Transaction>(request)) : validation)}";
        }
    }
}
