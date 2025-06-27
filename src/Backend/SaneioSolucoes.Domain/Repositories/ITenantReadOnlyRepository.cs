namespace SaneioSolucoes.Domain.Repositories
{
    public interface ITenantReadOnlyRepository
    {
        public Task<Entities.Tenant?> GetTenantIdBySlug(string tenantSlug);
        public Task<bool> ExistsActiveTenantBySlugOrEmail(string tenantSlug, string email);
    }
}
