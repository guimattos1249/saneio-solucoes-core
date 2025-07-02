namespace SaneioSolucoes.Domain.Entities
{
    public class Bank : EntityBase
    {
        public string LegalName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
    }
}
