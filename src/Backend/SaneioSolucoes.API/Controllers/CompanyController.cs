using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.API.Attributes;
using SaneioSolucoes.Application.UseCases.Company.GetAll;
using SaneioSolucoes.Application.UseCases.Company.GetById;
using SaneioSolucoes.Application.UseCases.Company.Register;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.API.Controllers
{
    [AuthenticatedUser]
    public class CompanyController : SaneioSolucoesBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredCompanyJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterCompanyUseCase useCase,
        [FromBody] RequestRegisterCompnay request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
        [FromServices] IGetCompanyByIdUseCase useCase,
        [FromRoute] Guid id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseCompaniesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
        [FromServices] IGetAllCompaniesUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Companies.Any())
                return Ok(response);

            return NoContent();
        }
    }
}
