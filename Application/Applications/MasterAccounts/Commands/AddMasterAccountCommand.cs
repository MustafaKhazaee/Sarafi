
using AutoMapper;
using FluentValidation;
using MediatR;
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;

namespace Sarafi.Application.Applications.MasterAccounts.Commands
{
    public class AddMasterAccountCommand : Mappable<AddMasterAccountCommand, MasterAccount>, IRequest<string>
    {
        public string MasterAccountName { set; get; }
        public string? Code { set; get; }
    }
    public class AddMasterAccountCommandHandler : IRequestHandler<AddMasterAccountCommand, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<AddMasterAccountCommand> _validator;
        private readonly IMapper _mapper;
        public AddMasterAccountCommandHandler (IUnitOfWork unit, IValidator<AddMasterAccountCommand> validator, IMapper mapper)
        {
            _uow = unit;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<string> Handle(AddMasterAccountCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            return $"{(validation.IsValid ? await _uow.MasterAccountRepository.AddAsync(_mapper.Map<MasterAccount>(request), cancellationToken) : validation)}";
        }
    }
}
