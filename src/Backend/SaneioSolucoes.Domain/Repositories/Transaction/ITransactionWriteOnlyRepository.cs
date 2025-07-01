using SaneioSolucoes.Domain.Dtos;

namespace SaneioSolucoes.Domain.Repositories.Transaction
{
    public interface ITransactionWriteOnlyRepository
    {
        Task AddRange(List<TransactionDto> transactions);
    }
}
