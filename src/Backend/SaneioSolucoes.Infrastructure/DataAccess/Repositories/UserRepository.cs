using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;

        public UserRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public Task<bool> ExistActiveUserWithEmail(Guid tenantId, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistActiveUserWithIdentifier(Guid tenantId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByEmailAndPassword(Guid tenantId, string email, string password) =>
            await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => 
                    user.Active && 
                    user.TenantId.Equals(tenantId) && 
                    user.Email.Equals(email) && 
                    user.Password.Equals(password));
    }
}
