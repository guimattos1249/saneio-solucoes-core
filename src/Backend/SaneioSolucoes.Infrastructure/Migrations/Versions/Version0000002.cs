using FluentMigrator;
using SaneioSolucoes.Domain.Enums;

namespace SaneioSolucoes.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TENANT_PLAN, "Adding Plan Column to Tenant")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Tenants")
                .AddColumn("Plan").AsString(255).Nullable();
        }
    }
}
