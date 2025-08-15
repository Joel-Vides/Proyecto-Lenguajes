using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Terminal.Database.Entities;
using Terminal.Database.Entities.Common;
using Terminal.Services.Interfaces;

namespace Terminal.Database
{
    public class TerminalDbContext : IdentityDbContext<UserEntity, RoleEntity, string>
    {

        private readonly IAudithService _audithService;

        public TerminalDbContext(DbContextOptions options,
            IAudithService audithService) : base(options)
        {
            _audithService = audithService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SetIdentityTablesNames(builder);

            // Relación: Company tiene muchos Buses
            builder.Entity<CompanyEntity>()
                .HasMany(c => c.Buses)
                .WithOne(b => b.Company)
                .HasForeignKey(b => b.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                ));

            foreach (var entityEntry in entries)
            {
                var entity = entityEntry.Entity as BaseEntity;
                if (entity != null)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        entity.CreateDate = DateTime.Now;
                        entity.CreatedBy = _audithService.GetUserId();
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdatedBy = _audithService.GetUserId();
                    }
                    else
                    {
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdatedBy = _audithService.GetUserId();
                    }
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        private static void SetIdentityTablesNames(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().ToTable("sec_users");
            builder.Entity<RoleEntity>().ToTable("sec_roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("sec_users_roles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserClaim<string>>().ToTable("sec_users_claims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("sec_roles_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("sec_users_logins");
            builder.Entity<IdentityUserToken<string>>().ToTable("sec_users_tokens");
        }

        public DbSet<CompanyEntity> Empresas { get; set; }
        public DbSet<BusEntity> Buses { get; set; }
        public DbSet<HorarioEntity> Horarios { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<RutaEntity> Rutas { get; set; }
    }
}
