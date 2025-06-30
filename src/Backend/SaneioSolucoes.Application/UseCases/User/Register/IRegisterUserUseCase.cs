using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
