namespace NationalLandmarks.Server.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data.Models;

    public class NationalLandmarksDbContext : IdentityDbContext<User>
    {
        public NationalLandmarksDbContext(DbContextOptions<NationalLandmarksDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Landmark>()
                .HasOne(l => l.User)
                .WithMany(u => u.Landmarks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}