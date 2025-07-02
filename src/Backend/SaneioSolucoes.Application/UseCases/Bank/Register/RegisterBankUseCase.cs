using AutoMapper;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories.Company;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Exceptions.ExceptionBase;
using SaneioSolucoes.Domain.Repositories.Bank;

namespace SaneioSolucoes.Application.UseCases.Bank.Register
{
    public class RegisterBankUseCase : IRegisterBankUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IBankWriteOnlyRepository _bankWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterBankUseCase(ILoggedUser loggedUser, IBankWriteOnlyRepository bankWriteOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _bankWriteOnlyRepository = bankWriteOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredBankJson> Execute(RequestRegisterBankJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var bank = _mapper.Map<Domain.Entities.Bank>(request);
            bank.TenantId = loggedUser.TenantId;

            await _bankWriteOnlyRepository.Add(bank);

            await _unitOfWork.Commit();

            return new ResponseRegisteredBankJson
            {
                Id = bank.Id,
                LegalName = bank.LegalName,
                Code = bank.Code,
            };
        }

        private static void Validate(RequestRegisterBankJson request)
        {
            var result = new RegisterBankValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
