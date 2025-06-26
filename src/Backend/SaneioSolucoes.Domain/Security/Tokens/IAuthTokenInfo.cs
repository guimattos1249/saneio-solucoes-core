namespace SaneioSolucoes.Domain.Security.Tokens
{
    public interface IAuthTokenInfo
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
