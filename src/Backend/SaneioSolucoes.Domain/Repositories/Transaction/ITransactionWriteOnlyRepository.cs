using SaneioSolucoes.Domain.Dtos;

namespace SaneioSolucoes.Domain.Repositories.Transaction
{
    public interface ITransactionWriteOnlyRepository
    {
        Task Add(List<TransactionDto> transactions);
    }
}
