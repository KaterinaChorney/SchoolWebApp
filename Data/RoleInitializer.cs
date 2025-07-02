using Microsoft.AspNetCore.Identity;
using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data
{
    public static class RoleInitializer
    {
        private static readonly string[] roles = new[] { "Admin", "Teacher", "Student" };

        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "admin@gmail.com";
            string adminPassword = "Der18!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    Email = adminEmail,
                    UserName = "admin",
                    DisplayName = "Адміністратор",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            string teacherEmail = "teacher@gmail.com";
            if (await userManager.FindByEmailAsync(teacherEmail) == null)
            {
                var teacher = new User
                {
                    Email = teacherEmail,
                    UserName = "teacher",
                    DisplayName = "Вчитель",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(teacher, "Teacher123!");
                await userManager.AddToRoleAsync(teacher, "Teacher");
            }

            string studentEmail = "student@gmail.com";
            if (await userManager.FindByEmailAsync(studentEmail) == null)
            {
                var student = new User
                {
                    Email = studentEmail,
                    UserName = "student",
                    DisplayName = "Учень",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(student, "Stu123!");
                await userManager.AddToRoleAsync(student, "Student");
            }
        }
    }
}