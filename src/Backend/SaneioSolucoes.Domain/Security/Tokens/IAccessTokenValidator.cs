namespace SaneioSolucoes.Domain.Security.Tokens
{
    public interface IAccessTokenValidator
    {
        public IAuthTokenInfo ValidateAndGetUserIdentifier(string token);
    }
}
