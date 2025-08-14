using Microsoft.EntityFrameworkCore;
using Terminal.API.Database.Entities;
using Terminal.Database.Entities;

namespace Terminal.Database
{
    public class TerminalDbContext : DbContext
    {
        public TerminalDbContext(DbContextOptions<TerminalDbContext> options)
            : base(options)
        {
        }

        public DbSet<CompanyEntity> Empresas { get; set; }
        public DbSet<BusEntity> Buses { get; set; }
        public DbSet<HorarioEntity> Horarios { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<RutaEntity> Rutas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación: Company tiene muchos Buses
            modelBuilder.Entity<CompanyEntity>()
                .HasMany(c => c.Buses)
                .WithOne(b => b.Company)
                .HasForeignKey(b => b.CompanyId)
                .OnDelete(DeleteBehavior.Cascade); // Elimina los buses si se elimina la empresa

            base.OnModelCreating(modelBuilder);
        }
    }
}
