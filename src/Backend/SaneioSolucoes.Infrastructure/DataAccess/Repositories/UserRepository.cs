using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories.User;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;

        public UserRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public async Task Add(User User) =>
            await _dbContext.Users.AddAsync(User);

        public async Task<bool> ExistActiveUserWithEmail(Guid tenantId, string email) =>
            await _dbContext
            .Users
            .AnyAsync(
                user => user.Active && user.TenantId.Equals(tenantId) && user.Email.Equals(email));

        public async Task<bool> ExistActiveUserWithIdentifier(Guid tenantId, Guid userId) =>
            await _dbContext
                    .Users
                    .AsNoTracking()
                    .AnyAsync(user => user.Active && user.Id == userId && user.TenantId == tenantId);

        public async Task<User?> GetUserByEmailAndPassword(Guid tenantId, string email, string password) =>
            await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => 
                    user.Active && 
                    user.TenantId.Equals(tenantId) && 
                    user.Email.Equals(email) && 
                    user.Password.Equals(password));

        public async Task<User> GetById(Guid userId, Guid tenantId) =>
            await _dbContext
                    .Users
                    .FirstAsync(user => user.Active && user.Id == userId && user.TenantId == tenantId);

        public void Update(User user) => _dbContext.Users.Update(user);
    }
}
