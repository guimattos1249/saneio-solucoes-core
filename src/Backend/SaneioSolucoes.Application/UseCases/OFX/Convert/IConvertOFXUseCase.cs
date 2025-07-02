using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.OFX.Convert
{
    public interface IConvertOFXUseCase
    {
        public Task<ResponseExportResult> Execute(RequestOFXFileConverter request, Guid companyId, Guid bankId);
    }
}
