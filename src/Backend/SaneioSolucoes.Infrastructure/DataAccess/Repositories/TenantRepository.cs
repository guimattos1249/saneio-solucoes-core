using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    public class TenantRepository : ITenantReadOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;

        public TenantRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public async Task<Tenant?> GetTenantIdByslug(string tenantSlug) => 
            await _dbContext.Tenants
                .AsNoTracking()
                .FirstOrDefaultAsync(tenant => tenant.Slug.Equals(tenantSlug));
    }
}
