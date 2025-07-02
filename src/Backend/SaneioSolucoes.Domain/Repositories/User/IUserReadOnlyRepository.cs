namespace SaneioSolucoes.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(Guid tenantId, string email);
        public Task<Entities.User?> GetUserByEmailAndPassword(Guid tenantId, string email, string password);
        public Task<bool> ExistActiveUserWithIdentifier(Guid tenantId, Guid userId);

        Task<Dictionary<Guid, string>> GetNameDictionary(IEnumerable<Guid> ids);
    }
}
