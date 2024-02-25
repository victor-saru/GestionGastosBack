using GestionGastosBD.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionGastosBD
{
    public class GestionGastosBDContext : DbContext
    {
        public GestionGastosBDContext(DbContextOptions<GestionGastosBDContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<Participants> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .ToTable("users");

            modelBuilder.Entity<Expenses>()
                .ToTable("expenses");

            modelBuilder.Entity<Expenses>()
                .Property(e => e.cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PaymentMethods>()
                .ToTable("payment_methods");

            modelBuilder.Entity<Participants>()
                .ToTable("participants");

            modelBuilder.Entity<Participants>()
                .Property(p => p.net_monthly_salary)
                .HasPrecision(10, 2);
                
        }
    }
}
