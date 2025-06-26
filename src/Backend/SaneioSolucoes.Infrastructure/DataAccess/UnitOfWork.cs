using SaneioSolucoes.Domain.Repositories;

namespace SaneioSolucoes.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SaneioSolucoesDBContext _dbContext;

        public UnitOfWork(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
