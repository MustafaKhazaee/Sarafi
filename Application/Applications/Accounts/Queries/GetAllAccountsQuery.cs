
using AutoMapper;
using MediatR;
using Sarafi.Application.Applications.Accounts.Dtos;
using Sarafi.Application.Interfaces.Repositories;

namespace Sarafi.Application.Applications.Accounts.Queries
{
    public class GetAllAccountsQuery : IRequest<List<AccountDto>> { }
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllAccountsQueryHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var a = await _uow.AccountRepository.GetAllAsync(cancellationToken);
            
            return _mapper.Map<List<AccountDto>>(a);
        }
    }
}
