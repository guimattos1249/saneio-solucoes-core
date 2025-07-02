using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaneioSolucoes.Application.Services.AutoMapper;
using SaneioSolucoes.Application.UseCases.Bank.GetAll;
using SaneioSolucoes.Application.UseCases.Bank.GetById;
using SaneioSolucoes.Application.UseCases.Bank.Register;
using SaneioSolucoes.Application.UseCases.Company.GetAll;
using SaneioSolucoes.Application.UseCases.Company.GetById;
using SaneioSolucoes.Application.UseCases.Company.Register;
using SaneioSolucoes.Application.UseCases.Login.DoLogin;
using SaneioSolucoes.Application.UseCases.OFX.Convert;
using SaneioSolucoes.Application.UseCases.Tenant.Register;
using SaneioSolucoes.Application.UseCases.User.ChangePassword;
using SaneioSolucoes.Application.UseCases.User.Profile;
using SaneioSolucoes.Application.UseCases.User.Register;

namespace SaneioSolucoes.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AutoMapper(services);
            AddUseCases(services);
        }

        private static void AutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
            {
                autoMapperOptions.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IRegisterTenantUseCase, RegisterTenantUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IConvertOFXUseCase, ConvertOFXUseCase>();
            services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
            services.AddScoped<IGetCompanyByIdUseCase, GetCompanyByIdUseCase>();
            services.AddScoped<IGetAllCompaniesUseCase, GetAllCompaniesUseCase>();
            services.AddScoped<IRegisterBankUseCase, RegisterBankUseCase>();
            services.AddScoped<IGetBankByIdUseCase, GetBankByIdUseCase>();
            services.AddScoped<IGetAllBanksUseCase, GetAllBanksUseCase>();
        }
    }
}
