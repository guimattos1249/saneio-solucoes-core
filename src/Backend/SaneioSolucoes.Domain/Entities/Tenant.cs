using SaneioSolucoes.Domain.Enums;

namespace SaneioSolucoes.Domain.Entities
{
    public class Tenant : EntityBase
    {
        public string TradeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public PlanType Plan { get; set; }
    }
}
