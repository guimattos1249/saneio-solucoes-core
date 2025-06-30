using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Exceptions.ExceptionBase;
using SaneioSolucoes.Exceptions;
using SaneioSolucoes.Domain.Repositories;
using AutoMapper;
using SaneioSolucoes.Domain.Security.Tokens;
using SaneioSolucoes.Domain.Security.Cryptography;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Repositories.Tenant;
using SaneioSolucoes.Domain.Repositories.User;

namespace SaneioSolucoes.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly ITenantReadOnlyRepository _tenantReadOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IPasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(
            ITenantReadOnlyRepository tenantReadOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IAccessTokenGenerator accessTokenGenerator, 
            IPasswordEncripter passwordEncripter)
        {
            _tenantReadOnlyRepository = tenantReadOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessTokenGenerator = accessTokenGenerator;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            var tenant = await _tenantReadOnlyRepository.GetTenantIdBySlug(request.TenantSlug) ?? throw new SaneioSolucoesException(ResourceMessageExceptions.INVALID_TENANT_SLUG);

            await Validate(tenant.Id, request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.TenantId = tenant.Id;

            await _userWriteOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokenJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.Id, user.TenantId),
                }
            };

        }

        private async Task Validate(Guid tenantId, RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(tenantId, request.Email);

            if (emailExist)
                result.Errors.Add(
                    new FluentValidation.Results.ValidationFailure(
                        string.Empty, ResourceMessageExceptions.EMAIL_ALREADY_REGISTERED));

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
