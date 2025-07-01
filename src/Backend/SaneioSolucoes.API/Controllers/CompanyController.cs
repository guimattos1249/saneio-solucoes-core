using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.Application.UseCases.Company.Register;
using SaneioSolucoes.Application.UseCases.User.Register;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.API.Controllers
{
    public class CompanyController : SaneioSolucoesBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterCompanyUseCase useCase,
        [FromBody] RequestRegisterCompnay request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}
