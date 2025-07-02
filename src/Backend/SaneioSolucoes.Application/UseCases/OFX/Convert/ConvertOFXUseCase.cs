using SaneioSolucoes.Application.Services.OFX;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Repositories.Bank;
using SaneioSolucoes.Domain.Repositories.Company;
using SaneioSolucoes.Domain.Repositories.Transaction;
using SaneioSolucoes.Domain.Repositories.User;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Domain.Services.OFX;
using SaneioSolucoes.Domain.Services.XLS;
using SaneioSolucoes.Exceptions;
using SaneioSolucoes.Exceptions.ExceptionBase;

namespace SaneioSolucoes.Application.UseCases.OFX.Convert
{
    public class ConvertOFXUseCase : IConvertOFXUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IOFXParser _ofXParser;
        private readonly IXLSGenerator _xLSGenerator;
        private readonly ITransactionWriteOnlyRepository _transactionWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;
        private readonly IBankReadOnlyRepository _bankReadOnlyRepository;

        public ConvertOFXUseCase(
            ILoggedUser loggedUser,
            IOFXParser ofXParser,
            IXLSGenerator xLSGenerator,
            ITransactionWriteOnlyRepository transactionWriteOnlyRepository,
            IUnitOfWork unitOfWork,
            IUserReadOnlyRepository userReadOnlyRepository,
            ICompanyReadOnlyRepository companyReadOnlyRepository,
            IBankReadOnlyRepository bankReadOnlyRepository)
        {
            _loggedUser = loggedUser;
            _ofXParser = ofXParser;
            _xLSGenerator = xLSGenerator;
            _transactionWriteOnlyRepository = transactionWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
            _companyReadOnlyRepository = companyReadOnlyRepository;
            _bankReadOnlyRepository = bankReadOnlyRepository;
        }

        public async Task<ResponseExportResult> Execute(RequestOFXFileConverter request, Guid companyId, Guid bankId)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            if (request?.File == null || request.File.Length == 0)
                throw new SaneioSolucoesException(ResourceMessageExceptions.EMPTY_FILE);

            using var reader = new StreamReader(request.File!.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            var transactions = _ofXParser.Parse(content);

            transactions.ForEach(transaction => {
                transaction.UserId = loggedUser.Id;
                transaction.TenantId = loggedUser.TenantId;
                transaction.CompanyId = companyId;
                transaction.BankId = bankId;
            });

            if (transactions.Count == 0)
                throw new SaneioSolucoesException(ResourceMessageExceptions.NO_TRANSACTIONS_ON_FILE);

            await _transactionWriteOnlyRepository.AddRange(transactions);

            await _unitOfWork.Commit();

            var companyDict = await _companyReadOnlyRepository.GetTradeNameDictionary(new[] { companyId });
            var userDict = await _userReadOnlyRepository.GetNameDictionary(new[] { loggedUser.Id });
            var bankDict = await _bankReadOnlyRepository.GetLegalNameDictionary(new[] { bankId });

            var exportData = TransactionExportHelper.PrepareExportData(transactions, companyDict, userDict, bankDict);

            var fileContent = _xLSGenerator.GenerateXLS(exportData);

            var fileName = $"Extrato_{exportData.First().CompanyName}_{exportData.First().BankName}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return new ResponseExportResult
            {
                FileName = fileName,
                FileContent = fileContent
            };
        }

        private static void Validate(RequestOFXFileConverter request)
        {
            var result = new ConvertOFXValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
