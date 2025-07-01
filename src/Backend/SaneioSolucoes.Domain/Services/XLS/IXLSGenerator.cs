using SaneioSolucoes.Domain.Dtos;

namespace SaneioSolucoes.Domain.Services.XLS
{
    public interface IXLSGenerator
    {
        byte[] GenerateXLS(List<TransactionDto> transactions);
    }
}
