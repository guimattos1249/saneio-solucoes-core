using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Security.Tokens;

namespace SaneioSolucoes.Infrastructure.Security.Tokens.Access.Validator
{
    public class JwtTokenValidator(string signingKey) : JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _signingKey = signingKey;

        public IAuthTokenInfo ValidateAndGetUserIdentifier(string token)
        {
            var validationParameter = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_signingKey),
                ClockSkew = new TimeSpan(0)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

            var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var tenantId = principal.Claims.First(c => c.Type == "tenant_id").Value;

            return new AuthTokenInfo
            {
                UserId = Guid.Parse(userIdentifier),
                TenantId = Guid.Parse(tenantId)
            };
        }
    }
}
