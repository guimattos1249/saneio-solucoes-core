namespace SaneioSolucoes.Domain.Repositories
{
    public interface ITenantReadOnlyRepository
    {
        public Task<Entities.Tenant?> GetTenantIdByslug(string tenantSlug);
    }
}
