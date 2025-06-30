namespace SaneioSolucoes.Domain.Repositories.Tenant
{
    public interface ITenantWriteOnlyRepository
    {
        public Task Add(Entities.Tenant tenant);
    }
}
