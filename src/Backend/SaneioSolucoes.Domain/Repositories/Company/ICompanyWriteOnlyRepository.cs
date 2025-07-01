namespace SaneioSolucoes.Domain.Repositories.Company
{
    public interface ICompanyWriteOnlyRepository
    {
        public Task Add(Entities.Company company);
    }
}
