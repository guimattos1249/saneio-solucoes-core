namespace SaneioSolucoes.Domain.Entities
{
    public class Company : EntityBase
    {
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Cnpj { get; set;} = string.Empty;
        public Guid TenentId { get; set; }
    }
}
