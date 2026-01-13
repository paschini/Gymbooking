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

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

            // Configure many-to-many relationship between ApplicationUser and GymClass
            //builder.Entity<ApplicationUser>()
            //    .HasMany(u => u.AttendedClasses)
            //    .WithMany(c => c.AttendingMembers)
            //    .UsingEntity(j => j.ToTable("ApplicationUserGymClasses"));

            //builder.Entity<ApplicationUserGymClass>()
            //    .HasKey(t => new { t.ApplicationUserId, t.GymClassId });
        //}
    }
}
