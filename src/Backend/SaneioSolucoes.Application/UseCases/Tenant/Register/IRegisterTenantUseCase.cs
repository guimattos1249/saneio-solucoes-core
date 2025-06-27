using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Tenant.Register
{
    public interface IRegisterTenantUseCase
    {
        public Task<ResponseRegisteredTenantJson> Execute(RequestRegisterTenantJson request);
    }
}
