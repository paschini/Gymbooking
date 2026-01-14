using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Gymbooking.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        //Navigation property for the many-to-many relationship with GymClass
        public ICollection<GymClass> AttendedClasses { get; set; }

     }
}

