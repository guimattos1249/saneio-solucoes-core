using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Security.Tokens;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Infrastructure.DataAccess;

namespace SaneioSolucoes.Infrastructure.Services.LoggedUser
{
    public class LoggedUser(SaneioSolucoesDBContext dBContext, ITokenProvider tokenProvider) : ILoggedUser
    {
        private readonly SaneioSolucoesDBContext _dbContext = dBContext;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        public async Task<User> User()
        {
            var token = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var userId = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var tenantId = jwtSecurityToken.Claims.First(c => c.Type == "tenant_id").Value;

            var userGuidId = Guid.Parse(userId);

            var loggedUser = await _dbContext
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.Active && user.Id.Equals(userGuidId) && user.TenantId.Equals(tenantId));

            return loggedUser;
        }
    }
}
