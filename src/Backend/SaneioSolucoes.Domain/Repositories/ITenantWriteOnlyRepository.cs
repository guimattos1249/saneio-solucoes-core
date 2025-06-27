namespace SaneioSolucoes.Domain.Repositories
{
    public interface ITenantWriteOnlyRepository
    {
        public Task Add(Entities.Tenant tenant);
    }
}
