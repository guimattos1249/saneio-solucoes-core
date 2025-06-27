using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    public class TenantRepository : ITenantReadOnlyRepository, ITenantWriteOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;

        public TenantRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public async Task Add(Tenant tenant) => await _dbContext.Tenants.AddAsync(tenant);

        public async Task<bool> ExistsActiveTenantBySlugOrEmail(string tenantSlug, string email) =>
            await _dbContext.Tenants
                .AnyAsync(tenant => tenant.Slug.Equals(tenantSlug) || tenant.Email.Equals(email));

        public async Task<Tenant?> GetTenantIdBySlug(string tenantSlug) => 
            await _dbContext.Tenants
                .AsNoTracking()
                .FirstOrDefaultAsync(tenant => tenant.Slug.Equals(tenantSlug));
    }
}
