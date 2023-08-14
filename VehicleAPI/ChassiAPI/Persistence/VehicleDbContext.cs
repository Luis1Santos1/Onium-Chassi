using Microsoft.EntityFrameworkCore;
using VehicleAPI.Entities;

namespace VehicleAPI.Entities
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para mapear a propriedade ChassiBinary como varbinary
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.Chassi)
                .HasColumnType("nvarchar(max)");
        }

    }
}
