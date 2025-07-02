namespace SaneioSolucoes.Domain.Repositories.Company
{
    public interface ICompanyReadOnlyRepository
    {
        Task<IList<Entities.Company>> GetAll(Guid tenantId);

        Task<Entities.Company?> GetById(Guid tenantId, Guid companyId);

        Task<Dictionary<Guid, string>> GetTradeNameDictionary(IEnumerable<Guid> ids);
    }
}
