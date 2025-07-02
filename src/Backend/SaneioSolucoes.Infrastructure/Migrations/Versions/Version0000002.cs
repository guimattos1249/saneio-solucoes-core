using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaneioSolucoes.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.BANK, "Creating bank and updating transaction")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Transactions")
                .AlterColumn("TransactionId").AsString(255).Nullable();

            CreateTable("Banks")
                .WithColumn("LegalName").AsString(255).NotNullable()
                .WithColumn("Code").AsString(255).NotNullable()
                .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_Bank_Tenant_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
