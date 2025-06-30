namespace SaneioSolucoes.Domain.Repositories.Tenant
{
    public interface ITenantReadOnlyRepository
    {
        public Task<Entities.Tenant?> GetTenantIdBySlug(string tenantSlug);
        public Task<bool> ExistsActiveTenantBySlugOrEmail(string tenantSlug, string email);
    }
}
