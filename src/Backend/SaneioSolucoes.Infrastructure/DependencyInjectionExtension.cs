using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Repositories.Tenant;
using SaneioSolucoes.Domain.Repositories.Transaction;
using SaneioSolucoes.Domain.Repositories.User;
using SaneioSolucoes.Domain.Security.Cryptography;
using SaneioSolucoes.Domain.Security.Tokens;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Domain.Services.OFX;
using SaneioSolucoes.Domain.Services.XLS;
using SaneioSolucoes.Infrastructure.DataAccess;
using SaneioSolucoes.Infrastructure.DataAccess.Repositories;
using SaneioSolucoes.Infrastructure.Extensions;
using SaneioSolucoes.Infrastructure.Security.Cryptography;
using SaneioSolucoes.Infrastructure.Security.Tokens.Access.Generator;
using SaneioSolucoes.Infrastructure.Security.Tokens.Access.Validator;
using SaneioSolucoes.Infrastructure.Services.LoggedUser;
using SaneioSolucoes.Infrastructure.Services.OFX;
using SaneioSolucoes.Infrastructure.Services.XLS;
using System.Reflection;

namespace SaneioSolucoes.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordEncrypter(services, configuration);
            AddRepositories(services);
            AddTokens(services, configuration);
            AddLoggedUser(services);
            AddServices(services);

            if (configuration.IsUnitTestEnviroment())
                return;

            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SaneioSolucoesDBContext>(dbContextOptions =>
            {
                var connectionString = configuration.ConnectionString();
                //var serverVersion = new MySqlServerVersion(new Version(8, 0, 42));
                //dbContextOptions.UseMySql(connectionString, serverVersion);
                dbContextOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
            services.AddScoped<ITenantReadOnlyRepository, TenantRepository>();
            services.AddScoped<ITenantWriteOnlyRepository, TenantRepository>();
            services.AddScoped<ITransactionWriteOnlyRepository, TransactionRepository>();
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                var connectionString = configuration.ConnectionString();
                options.AddMySql8()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("SaneioSolucoes.Infrastructure")).For.All();
            });
        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }

        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped<IPasswordEncripter>(options => new Sha512Encripter(additionalKey!));
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IOFXParser, OfxParser>();
            services.AddScoped<IXLSGenerator, XLSXGenerator>();
        }
    }
}
