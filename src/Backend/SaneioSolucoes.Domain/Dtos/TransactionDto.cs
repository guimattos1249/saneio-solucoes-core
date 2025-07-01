using SaneioSolucoes.Domain.Enums;

namespace SaneioSolucoes.Domain.Dtos
{
    public class TransactionDto
    {
        public DateTime? Date { get; set; }
        public string? Memo { get; set; } = string.Empty;
        public long Amount { get; set; }
        public string? Bank { get; set; } = string.Empty;
        public string? TransactionId { get; set; } = string.Empty;
        public string ServerTransactionId { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
        public TransactionType? Type { get; set; }
        public Guid CompanyId { get; set; }
        public Guid BankId { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
