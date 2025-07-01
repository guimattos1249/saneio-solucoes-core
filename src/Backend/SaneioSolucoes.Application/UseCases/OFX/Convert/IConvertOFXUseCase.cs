using SaneioSolucoes.Communication.Requests;

namespace SaneioSolucoes.Application.UseCases.OFX.Convert
{
    public interface IConvertOFXUseCase
    {
        public Task<byte[]> Execute(RequestOFXFileConverter request, Guid companyId, Guid bankId);
    }
}
