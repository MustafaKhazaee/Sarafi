
using MediatR;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;

namespace Sarafi.Application.Applications.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<List<Company>> { }
    public class GetAllCompaiesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<Company>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllCompaiesQueryHandler (IUnitOfWork unit) => _uow = unit;
        public async Task<List<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
            => await _uow.CompanyRepository.GetAllAsync(cancellationToken);
    }
}
