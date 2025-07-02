using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Bank.Register
{
    public interface IRegisterBankUseCase
    {
        public Task<ResponseRegisteredBankJson> Execute(RequestRegisterBankJson request);
    }
}
