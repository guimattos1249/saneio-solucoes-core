using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SaneioSolucoes.Application.UseCases.Login.DoLogin;
using SaneioSolucoes.Application.Services.AutoMapper;
using SaneioSolucoes.Application.UseCases.Tenant.Register;

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
        }
    }
}
