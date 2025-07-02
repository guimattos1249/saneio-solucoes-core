using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Company.GetAll
{
    public interface IGetAllCompaniesUseCase
    {
        public Task<ResponseCompaniesJson> Execute();
    }
}
