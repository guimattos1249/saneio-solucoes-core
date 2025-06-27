using SaneioSolucoes.Communication.Enums;

namespace SaneioSolucoes.Communication.Requests
{
    public class RequestRegisterTenantJson
    {
        public string TradeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public PlanType Plan { get; set; }
    }
}
