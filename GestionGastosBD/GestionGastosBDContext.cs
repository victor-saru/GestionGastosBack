using GestionGastosBD.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionGastosBD
{
    public class GestionGastosBDContext : DbContext
    {
        public GestionGastosBDContext(DbContextOptions<GestionGastosBDContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .ToTable("users");
        }
    }
}
