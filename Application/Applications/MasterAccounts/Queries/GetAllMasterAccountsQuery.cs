
using MediatR;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;

namespace Sarafi.Application.Applications.MasterAccounts.Queries
{
    public class GetAllMasterAccountsQuery : IRequest<List<MasterAccount>> { }
    public class GetAllMasterAccountsQueryHandler : IRequestHandler<GetAllMasterAccountsQuery, List<MasterAccount>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllMasterAccountsQueryHandler (IUnitOfWork unit) => _uow = unit;
        public async Task<List<MasterAccount>> Handle(GetAllMasterAccountsQuery request, CancellationToken cancellationToken)
            => await _uow.MasterAccountRepository.GetAllAsync(cancellationToken);
    }
}
