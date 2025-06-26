using AutoMapper;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Communication.Responses;

namespace SaneioSolucoes.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {

        public AutoMapping() 
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
        }

        private void DomainToResponse()
        {
        }
    }
}
