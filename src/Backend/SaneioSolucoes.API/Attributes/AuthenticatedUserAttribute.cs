using Microsoft.AspNetCore.Mvc;
using SaneioSolucoes.API.Filters;

namespace SaneioSolucoes.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
