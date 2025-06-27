using SaneioSolucoes.Communication.Enums;

namespace SaneioSolucoes.Communication.Responses
{
    public class ResponseRegisteredTenantJson
    {
        public string TradeName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public PlanType Plan { get; set; }
    }
}
