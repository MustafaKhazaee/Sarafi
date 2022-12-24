
using AutoMapper;
using FluentValidation;
using MediatR;
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Companies.Commands
{
    public class AddCompanyCommand : Mappable<AddCompanyCommand, Company>, IRequest<string>
    {
        public string Name { set; get; }
        public string? Address { set; get; }
        public string? Market { set; get; }
        public string? Floor { set; get; }
        public string? Room { set; get; }
        public string? Email { set; get; }
        public string? Mobile { set; get; }
        public string? Logo { set; get; }
        public Country Country { set; get; }
        public long ProvinceId { set; get; }
    }
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCompanyCommand> _validator;
        public AddCompanyCommandHandler (IUnitOfWork unitOfWork, IValidator<AddCompanyCommand> validator, IMapper mapper)
        {
            _uow = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<string> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            return $"{(validation.IsValid ? await _uow.CompanyRepository.AddAsync(_mapper.Map<Company>(request)) : validation)}";
        }
    }
}
