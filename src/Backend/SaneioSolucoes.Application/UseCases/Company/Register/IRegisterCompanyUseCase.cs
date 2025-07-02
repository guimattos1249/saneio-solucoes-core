using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Company.Register
{
    public interface IRegisterCompanyUseCase
    {
        public Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompnay request);
    }
}
