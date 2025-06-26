using SaneioSolucoes.Domain.Entities;

namespace SaneioSolucoes.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
