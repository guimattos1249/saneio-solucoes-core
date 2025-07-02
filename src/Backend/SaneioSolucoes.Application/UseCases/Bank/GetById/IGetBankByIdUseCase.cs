using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Bank.GetById
{
    public interface IGetBankByIdUseCase
    {
        public Task<ResponseRegisteredBankJson> Execute(Guid id);
    }
}
