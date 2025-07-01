using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Repositories.Transaction;
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

        public ConvertOFXUseCase(
            ILoggedUser loggedUser,
            IOFXParser ofXParser,
            IXLSGenerator xLSGenerator,
            ITransactionWriteOnlyRepository transactionWriteOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _ofXParser = ofXParser;
            _xLSGenerator = xLSGenerator;
            _transactionWriteOnlyRepository = transactionWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Execute(RequestOFXFileConverter request, Guid companyId, Guid bankId)
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

            await _transactionWriteOnlyRepository.Add(transactions);

            await _unitOfWork.Commit();

            return _xLSGenerator.GenerateXLS(transactions);
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
