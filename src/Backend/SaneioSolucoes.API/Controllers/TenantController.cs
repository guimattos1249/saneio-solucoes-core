using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.Application.UseCases.Login.DoLogin;
using SaneioSolucoes.Application.UseCases.Tenant.Register;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.API.Controllers
{
    public class TenantController : SaneioSolucoesBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredTenantJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterTenantUseCase useCase,
        [FromBody] RequestRegisterTenantJson request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}
