using Microsoft.AspNetCore.Http;

namespace SaneioSolucoes.Communication.Requests
{
    public class RequestOFXFileConverter
    {
        public IFormFile? File { get; set; }
    }
}
