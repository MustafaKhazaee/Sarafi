
using AutoMapper;
using MediatR;
using Sarafi.Application.Applications.Transactions.Dtos;
using Sarafi.Application.Interfaces.Repositories;

namespace Sarafi.Application.Applications.Transactions.Queries
{
    public class GetAllTransactionsQuery : IRequest<List<TransactionDto>>
    {
    }
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllTransactionsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken) =>
            _mapper.Map<List<TransactionDto>>(await _uow.TransactionRepository.GetAllAsync(cancellationToken));
    }
}
