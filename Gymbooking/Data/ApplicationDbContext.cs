using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Gymbooking.Models;
using Microsoft.AspNetCore.Identity;

namespace Gymbooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    //public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
    //public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<GymClass> GymClasses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Shadow property to track the time of user registration
            builder.Entity<ApplicationUser>()
            .Property<DateTime>("TimeOfRegistration");
        }

        public override int SaveChanges()
        {
            SetTimeOfRegistration();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimeOfRegistration();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetTimeOfRegistration()
        {
            var entries = ChangeTracker.Entries<ApplicationUser>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                entry.Property("TimeOfRegistration").CurrentValue = DateTime.Now; // or UtcNow
            }
        }
    }
}
