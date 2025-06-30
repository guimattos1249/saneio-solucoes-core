using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
