using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.API.Attributes;
using SaneioSolucoes.Application.UseCases.User.ChangePassword;
using SaneioSolucoes.Application.UseCases.User.Profile;
using SaneioSolucoes.Application.UseCases.User.Register;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.API.Controllers
{
    public class UserController : SaneioSolucoesBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request)
        {
            await useCase.Execute(request);

            return NoContent();
        }
    }
}
