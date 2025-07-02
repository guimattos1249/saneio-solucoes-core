using SaneioSolucoes.Domain.Dtos;

namespace SaneioSolucoes.Application.Services.OFX
{
    public static class TransactionExportHelper
    {
        public static List<TransactionExportDto> PrepareExportData(
            List<TransactionDto> transactions,
            Dictionary<Guid, string> companyDict,
            Dictionary<Guid, string> userDict,
            Dictionary<Guid, string> bankDict)
        {
            if (!transactions.Any())
                return new List<TransactionExportDto>();

            var companyName = companyDict.GetValueOrDefault(transactions.First().CompanyId, "Desconhecido");
            var userName = userDict.GetValueOrDefault(transactions.First().UserId, "Desconhecido");
            var bankName = bankDict.GetValueOrDefault(transactions.First().BankId, "Desconhecido");

            return transactions.Select(t => new TransactionExportDto
            {
                Date = t.Date,
                Memo = t.Memo,
                Amount = t.Amount,
                TransactionId = t.TransactionId,
                CompanyName = companyName,
                UserName = userName,
                BankName = bankName
            }).ToList();
        }
    }
}
