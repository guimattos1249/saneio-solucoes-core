namespace SaneioSolucoes.Communication.Requests
{
    public class RequestLoginJson
    {
        public string TenantSlug { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
