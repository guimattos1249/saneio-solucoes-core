using FluentMigrator;
using SaneioSolucoes.Domain.Enums;

namespace SaneioSolucoes.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.INITIAL_TABLES, "Create table to save Comapny, Transaction and user information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Tenants")
                .WithColumn("TradeName").AsString(255).NotNullable()
                .WithColumn("Description").AsString(255).NotNullable()
                .WithColumn("DocumentNumber").AsString(255).NotNullable()
                .WithColumn("Slug").AsString(255).NotNullable().Unique()
                .WithColumn("Email").AsString(255).NotNullable();

            CreateTable("Users")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_User_Tenant_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("Companies")
                .WithColumn("LegalName").AsString(255).NotNullable()
                .WithColumn("TradeName").AsString(255).NotNullable()
                .WithColumn("Cnpj").AsString(255).NotNullable()
                .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_Company_Tenant_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("Transactions")
                .WithColumn("Date").AsDate().NotNullable()
                .WithColumn("Memo").AsString(255).NotNullable()
                .WithColumn("Amount").AsInt64().NotNullable()
                .WithColumn("Bank").AsString(255).NotNullable()
                .WithColumn("TransactionId").AsString(255).NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Transacition_User_Id", "Users", "Id")
                .WithColumn("CompanyId").AsGuid().NotNullable().ForeignKey("FK_Transacition_Company_Id", "Companies", "Id")
                .WithColumn("TenentId").AsGuid().NotNullable().ForeignKey("FK_Transacition_Tenant_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
