using Microsoft.AspNetCore.Identity;

namespace Gymbooking.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Navigation property for the many-to-many relationship with GymClass
        public ICollection<GymClass> AttendedClasses { get; set; }
    }
}

