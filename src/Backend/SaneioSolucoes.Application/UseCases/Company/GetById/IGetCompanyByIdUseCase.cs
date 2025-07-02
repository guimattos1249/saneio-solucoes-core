using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.UseCases.Company.GetById
{
    public interface IGetCompanyByIdUseCase
    {
        public Task<ResponseCompanyJson> Execute(Guid id);
    }
}
