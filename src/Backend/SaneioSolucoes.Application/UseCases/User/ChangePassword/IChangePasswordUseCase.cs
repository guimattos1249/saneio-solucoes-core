using SaneioSolucoes.Communication.Requests;

namespace SaneioSolucoes.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePasswordJson request);
    }
}
