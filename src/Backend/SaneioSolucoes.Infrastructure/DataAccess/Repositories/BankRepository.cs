using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories.Bank;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    public class BankRepository : IBankWriteOnlyRepository, IBankReadOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;
        public BankRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public async Task Add(Bank bank) => await _dbContext.Banks.AddAsync(bank);

        public async Task<IList<Bank>> GetAll(Guid tenantId) =>
            await _dbContext
                .Banks
                .AsNoTracking()
                .Where(bank => bank.Active && bank.TenantId == tenantId)
                .OrderByDescending(bank => bank.CreatedOn)
                .ToListAsync();

        public async Task<Bank?> GetById(Guid tenantId, Guid bankId) =>
            await _dbContext
                .Banks
                .AsNoTracking()
                .FirstOrDefaultAsync(bank => bank.Active &&
                                             bank.TenantId == tenantId &&
                                             bank.Id == bankId);

        public async Task<Dictionary<Guid, string>> GetLegalNameDictionary(IEnumerable<Guid> ids) =>
            await _dbContext.Banks
            .Where(b => ids.Contains(b.Id))
            .ToDictionaryAsync(b => b.Id, b => b.LegalName);
    }
}
