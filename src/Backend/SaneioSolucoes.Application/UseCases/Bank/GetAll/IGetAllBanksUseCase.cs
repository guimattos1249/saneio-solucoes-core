using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Bank.GetAll
{
    public interface IGetAllBanksUseCase
    {
        public Task<ResponseBanksJson> Execute();
    }
}
