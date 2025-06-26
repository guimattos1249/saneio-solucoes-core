using SaneioSolucoes.Domain.Security.Tokens;

namespace SaneioSolucoes.Infrastructure.Security.Tokens.Access
{
    public class AuthTokenInfo : IAuthTokenInfo
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
