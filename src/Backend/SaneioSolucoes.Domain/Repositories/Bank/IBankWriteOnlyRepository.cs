namespace SaneioSolucoes.Domain.Repositories.Bank
{
    public interface IBankWriteOnlyRepository
    {
        public Task Add(Entities.Bank bank);
    }
}
