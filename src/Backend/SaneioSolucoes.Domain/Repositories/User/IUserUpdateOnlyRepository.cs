namespace SaneioSolucoes.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        public Task<Entities.User> GetById(Guid userId, Guid tenantId);
        public void Update(Entities.User user);
    }
}
