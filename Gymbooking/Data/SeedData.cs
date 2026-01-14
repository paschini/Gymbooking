using Gymbooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;

namespace Gymbooking.Data
{
    public class SeedData
    {
        private static ApplicationDbContext _context = default!;
        private static RoleManager<IdentityRole> _roleManager = default!;
        private static UserManager<ApplicationUser> _userManager = default!;

        public static async Task Init(ApplicationDbContext context, IServiceProvider services)
        {
            _context = context;

            if (_context.Roles.Any()) { await Task.CompletedTask; }

            _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roleNames = new[] { "Admin", "Member" };

            var usersToAdd = new[]
            {
                (FirstName: "Admin", LastName: "Adminsson", Email: "admin@Gymbokning.se", Password: "Admin123!", Role: "Admin"),
                (FirstName: "Member", LastName: "Membersson", Email: "member@Gymbokning.se", Password: "Member123!", Role: "Member")
            };

            var usersToAssignRoles = usersToAdd.Select(u => (u.Email, u.Role)).ToArray();   

            await AddRolesAsync(roleNames);
            await AddAccountAsync(usersToAdd);
            await AssignRoleAsync(usersToAssignRoles);
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }
            }
        }

        private static async Task AddAccountAsync((string firstName, string lastName, string Email, string Password, string Role)[] users)
        {
            foreach (var (firstName, lastName, email, password, role) in users)
            {
                var userFound = await _userManager.FindByEmailAsync(email);
                if (userFound == null)
                {
                    userFound = new ApplicationUser
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(userFound, password);
                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }
            }
        }

        private static async Task AssignRoleAsync((string email, string role)[] userRoles)
        {
            foreach (var (email, role) in userRoles)
            {
                var userFound = await _userManager.FindByEmailAsync(email);
                var isInRole = await _userManager.IsInRoleAsync(userFound, role);

                if (!isInRole)
                {
                    var result = await _userManager.AddToRoleAsync(userFound, role);
                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }
            }
        }
    }
}
