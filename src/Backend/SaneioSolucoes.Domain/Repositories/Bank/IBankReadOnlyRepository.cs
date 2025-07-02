namespace SaneioSolucoes.Domain.Repositories.Bank
{
    public interface IBankReadOnlyRepository
    {
        Task<IList<Entities.Bank>> GetAll(Guid tenantId);

        Task<Entities.Bank?> GetById(Guid tenantId, Guid bankId);

        Task<Dictionary<Guid, string>> GetLegalNameDictionary(IEnumerable<Guid> ids);
    }
}
