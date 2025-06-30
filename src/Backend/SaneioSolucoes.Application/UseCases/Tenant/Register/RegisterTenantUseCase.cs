using AutoMapper;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Repositories.Tenant;
using SaneioSolucoes.Exceptions;
using SaneioSolucoes.Exceptions.ExceptionBase;

namespace SaneioSolucoes.Application.UseCases.Tenant.Register
{
    public class RegisterTenantUseCase : IRegisterTenantUseCase
    {
        private readonly ITenantWriteOnlyRepository _writeOnlyRepository;
        private readonly ITenantReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterTenantUseCase(
            ITenantWriteOnlyRepository writeOnlyRepository,
            ITenantReadOnlyRepository readOnlyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredTenantJson> Execute(RequestRegisterTenantJson request)
        {
            await Validate(request);

            var tenant = _mapper.Map<Domain.Entities.Tenant>(request);

            await _writeOnlyRepository.Add(tenant);

            await _unitOfWork.Commit();

            return new ResponseRegisteredTenantJson
            {
                TradeName = tenant.TradeName,
                Slug = tenant.Slug,
                Plan = (Communication.Enums.PlanType)tenant.Plan,
            };
        }

        private async Task Validate(RequestRegisterTenantJson request)
        {
            var validator = new RegisterTenantValidator();

            var result = validator.Validate(request);

            var existTenant = await _readOnlyRepository.ExistsActiveTenantBySlugOrEmail(request.Slug, request.Email);

            if (existTenant)
                result.Errors.Add(
                    new FluentValidation.Results.ValidationFailure(
                        string.Empty, ResourceMessageExceptions.TENANT_EMAIL_OR_SLUG_ALREADY_REGISTERED));

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
