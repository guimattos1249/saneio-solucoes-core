namespace SaneioSolucoes.Domain.Repositories
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(Guid tenantId, string email);
        public Task<Entities.User?> GetUserByEmailAndPassword(Guid tenantId, string email, string password);
        public Task<bool> ExistActiveUserWithIdentifier(Guid tenantId, Guid userId);
    }
}
