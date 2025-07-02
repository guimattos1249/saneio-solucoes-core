using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.API.Attributes;
using SaneioSolucoes.Application.UseCases.Bank.GetAll;
using SaneioSolucoes.Application.UseCases.Bank.GetById;
using SaneioSolucoes.Application.UseCases.Bank.Register;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.API.Controllers
{
    [AuthenticatedUser]
    public class BankController : SaneioSolucoesBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredBankJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterBankUseCase useCase,
        [FromBody] RequestRegisterBankJson request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseRegisteredBankJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
        [FromServices] IGetBankByIdUseCase useCase,
        [FromRoute] Guid id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseBanksJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
        [FromServices] IGetAllBanksUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Banks.Any())
                return Ok(response);

            return NoContent();
        }
    }
}
