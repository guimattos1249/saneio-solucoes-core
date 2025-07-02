using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories.Company;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    internal class CompanyRepository : ICompanyReadOnlyRepository, ICompanyWriteOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;
        public CompanyRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;
        public async Task Add(Company company) => await _dbContext.Companies.AddAsync(company);

        public async Task<IList<Company>> GetAll(Guid tenantId) =>
            await _dbContext
                .Companies
                .AsNoTracking()
                .Where(company => company.Active && company.TenantId == tenantId)
                .OrderByDescending(company => company.CreatedOn)
                .ToListAsync();

        public async Task<Company?> GetById(Guid tenantId, Guid companyId) =>
            await _dbContext
                .Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(company => company.Active &&
                                                company.TenantId == tenantId &&
                                                company.Id == companyId);
    }
}
