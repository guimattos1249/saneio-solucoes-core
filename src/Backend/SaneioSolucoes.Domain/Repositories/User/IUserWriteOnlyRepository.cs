namespace SaneioSolucoes.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Entities.User User);
    }
}
