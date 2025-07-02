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
            CreateMap<RequestRegisterTenantJson, Domain.Entities.Tenant>();
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<RequestRegisterCompnay, Domain.Entities.Company>();
            CreateMap<RequestRegisterBankJson, Domain.Entities.Bank>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<Domain.Entities.Company, ResponseCompanyJson>();
            CreateMap<Domain.Entities.Bank, ResponseRegisteredBankJson>();
        }
    }
}
