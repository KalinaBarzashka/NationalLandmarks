namespace NationalLandmarks.Server.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Data.Models.Base;
    using NationalLandmarks.Server.Infrastructure.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public class NationalLandmarksDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUserService;

        public NationalLandmarksDbContext(DbContextOptions<NationalLandmarksDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Landmark> Landmarks { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));//No action?

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Landmark>()
                .HasQueryFilter(l => !l.IsDeleted)
                .HasOne(l => l.User)
                .WithMany(u => u.Landmarks)
                .HasForeignKey(u => u.UserId);

            builder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);

            builder.Entity<Area>()
                .HasQueryFilter(a => !a.IsDeleted);

            builder.Entity<Town>()
                .HasQueryFilter(t => !t.IsDeleted);

            //builder.Entity<Visit>().HasKey(v => new { v.UserId, v.LandmarkId });
            builder.Entity<Visit>()
                .HasOne(u => u.User)
                .WithMany(v => v.Visits)
                .HasForeignKey(u => u.UserId);
            
            builder.Entity<Visit>()
                .HasOne(u => u.Landmark)
                .WithMany(v => v.Visits)
                .HasForeignKey(u => u.LandmarkId);


            base.OnModelCreating(builder);
        }

        private void ApplyAuditInfo()
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var userName = this.currentUserService.GetUserName();

                    if (entry.Entity is IDeletableEntity deletableEntity)
                    {
                        if (entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.DeletedBy = userName;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;
                            return;
                        }
                    }
                    
                    if (entry.Entity is IBaseEntity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedByUsername = userName;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModifiedOn = DateTime.UtcNow;
                            entity.ModifiedByUsername = userName;
                        }
                    }
                });
        }
    }
}