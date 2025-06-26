using Microsoft.AspNetCore.Mvc.Filters;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Security.Tokens;

namespace SaneioSolucoes.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;
        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
        {
            _accessTokenValidator = accessTokenValidator;
            _repository = repository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = TokenOnRequest(context);

            var authTokenInfo = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var existUser = await _repository.ExistActiveUserWithIdentifier(authTokenInfo.TenantId, authTokenInfo.UserId);

            if (existUser.IsFalse())
            {
                //throw new MyRecipeBookException(ResourceMessageExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
                throw new Exception("USER_WITHOUT_PERMISSION_ACCESS_RESOURCE");
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext!.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authentication))
            {
                //throw new MyRecipeBookException(ResourceMessageExceptions.NO_TOKEN);
                throw new Exception("No Token Provided");
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
