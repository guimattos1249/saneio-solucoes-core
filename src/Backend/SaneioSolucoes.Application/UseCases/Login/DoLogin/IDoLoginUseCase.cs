using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
