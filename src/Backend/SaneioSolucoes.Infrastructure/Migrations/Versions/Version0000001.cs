﻿using FluentMigrator;

namespace SaneioSolucoes.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.INITIAL_TABLES, "Create table to save Comapny, Transaction and user information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Tenants")
                .WithColumn("TradeName").AsString(255).NotNullable()
                .WithColumn("Description").AsString(255).Nullable()
                .WithColumn("DocumentNumber").AsString(255).NotNullable()
                .WithColumn("Slug").AsString(255).NotNullable().Unique()
                .WithColumn("Email").AsString(255).NotNullable().Unique()
                .WithColumn("Plan").AsInt32().NotNullable();

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
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("Memo").AsString(255).Nullable()
                .WithColumn("Amount").AsInt64().Nullable()
                .WithColumn("Bank").AsString(255).Nullable()
                .WithColumn("TransactionId").AsString(255).NotNullable()
                .WithColumn("ServerTransactionId").AsString(255).NotNullable()
                .WithColumn("AccountId").AsString(255).NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("Hash").AsString(2000).NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Transacition_User_Id", "Users", "Id")
                .WithColumn("CompanyId").AsGuid().NotNullable().ForeignKey("FK_Transacition_Company_Id", "Companies", "Id")
                .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_Transacition_Tenant_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
