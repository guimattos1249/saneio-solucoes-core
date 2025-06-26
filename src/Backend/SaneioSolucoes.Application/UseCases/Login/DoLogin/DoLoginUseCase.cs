using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Security.Cryptography;
using SaneioSolucoes.Domain.Security.Tokens;
using SaneioSolucoes.Exceptions.ExceptionBase;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        public readonly IUserReadOnlyRepository _userRepository;
        public readonly ITenantReadOnlyRepository _tenantRepository;
        public readonly IAccessTokenGenerator _accessTokenGenerator;
        public readonly IPasswordEncripter _passwordEncripter;

        public DoLoginUseCase(
            ITenantReadOnlyRepository tenantRepository,
            IUserReadOnlyRepository userRepository,
            IAccessTokenGenerator accessTokenGenerator,
            IPasswordEncripter passwordEncripter)
        {
            _tenantRepository = tenantRepository;
            _userRepository = userRepository;
            _accessTokenGenerator = accessTokenGenerator;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            request.Password = _passwordEncripter.Encrypt(request.Password);

            var tenant = await _tenantRepository.GetTenantIdByslug(request.TenantSlug);

            if (tenant == null)
                throw new SaneioSolucoesException(ResourceMessageExceptions.INVALID_TENANT_SLUG);

            var user = await _userRepository.GetUserByEmailAndPassword(tenant.Id, request.Email, request.Password);

            if (user == null)
                throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokenJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.Id, user.TenantId),
                }
            };
        }
    }
}
