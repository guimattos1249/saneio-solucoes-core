namespace SaneioSolucoes.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userIdentifier, Guid tenantId);
    }
}
