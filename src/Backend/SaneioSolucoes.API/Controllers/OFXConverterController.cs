using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.API.Attributes;
using SaneioSolucoes.Application.UseCases.OFX.Convert;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.API.Controllers
{
    [AuthenticatedUser]
    public class OFXConverterController : SaneioSolucoesBaseController
    {
        [HttpPost("{companyId}/{bankId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
        [FromServices] IConvertOFXUseCase useCase,
        [FromForm] RequestOFXFileConverter requestFrom,
        [FromRoute] Guid companyId,
        [FromRoute] Guid bankId)
        {
            var fileBytes = await useCase.Execute(requestFrom, companyId, bankId);

            return File(
                fileBytes,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"{companyId}_{bankId}_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            );
        }
    }
}
