using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Entities;

namespace SaneioSolucoes.Infrastructure.DataAccess
{
    public class SaneioSolucoesDBContext : DbContext
    {
        public SaneioSolucoesDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bank> Banks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaneioSolucoesDBContext).Assembly);
        }
    }
}
