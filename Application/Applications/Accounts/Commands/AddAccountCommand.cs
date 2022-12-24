
using AutoMapper;
using FluentValidation;
using MediatR;
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;
namespace Sarafi.Application.Applications.Accounts.Commands
{
    public class AddAccountCommand : Mappable<AddAccountCommand, Account>, IRequest<string>
    {
        public long MasterAccountId { set; get; }
        public long UserId { set; get; }
        public string AccountName { set; get; }
        public CurrencyType CurrencyType { set; get; }
    }
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AddAccountCommand> _validator;
        public AddAccountCommandHandler(IUnitOfWork unit, IMapper mapper, IValidator<AddAccountCommand> validator)
        {
            _uow = unit;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<string> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            return $"{(validation.IsValid ? await _uow.AccountRepository.AddAsync(_mapper.Map<Account>(request), cancellationToken) : validation)}";
        }
    }
}
