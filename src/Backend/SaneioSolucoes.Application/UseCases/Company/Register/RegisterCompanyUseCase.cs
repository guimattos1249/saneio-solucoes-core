using AutoMapper;
using SaneioSolucoes.Application.UseCases.OFX.Convert;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Repositories.Company;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Exceptions.ExceptionBase;

namespace SaneioSolucoes.Application.UseCases.Company.Register
{
    public class RegisterCompanyUseCase : IRegisterCompanyUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ICompanyWriteOnlyRepository _companyWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCompanyUseCase(ILoggedUser loggedUser, ICompanyWriteOnlyRepository companyWriteOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _companyWriteOnlyRepository = companyWriteOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompnay request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var company = _mapper.Map<Domain.Entities.Company>(request);

            await _companyWriteOnlyRepository.Add(company);

            await _unitOfWork.Commit();

            return new ResponseRegisteredCompanyJson
            {
                Id = company.Id,
                TradeName = company.TradeName,
                LegalName = company.LegalName,
                Cnpj = company.Cnpj,
            };
        }

        private static void Validate(RequestRegisterCompnay request)
        {
            var result = new RegisterCompanyValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
