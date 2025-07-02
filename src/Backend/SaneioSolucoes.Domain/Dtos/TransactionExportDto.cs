using SaneioSolucoes.Domain.Enums;

namespace SaneioSolucoes.Domain.Dtos
{
    public class TransactionExportDto
    {
        public DateTime? Date { get; set; }
        public string? Memo { get; set; } = string.Empty;
        public long Amount { get; set; }
        public string? Bank { get; set; } = string.Empty;
        public string? TransactionId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
    }
}
