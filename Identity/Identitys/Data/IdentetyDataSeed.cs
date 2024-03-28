using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickFix.DbContexts;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Identitys.Data
{
   
    public class IdentetyDataSeed
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentetyDataSeed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAllAsync()
        {
            await SeedRoles();
            await SeedUsers();
        }
        public int Order => 1;
        public static void MigrationsDb(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var contexts = services.GetRequiredService<AppDbContext>();
                contexts.Database.MigrateAsync().Wait();
            }
        }
        public static void SeedData(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                var seed = new IdentetyDataSeed(userManager, roleManager);
                seed.SeedAllAsync().Wait();
            }
        }

        private async Task SeedRoles()
        {

            if (!await _roleManager.RoleExistsAsync(ApplicationRole.Admin.Name))
                await _roleManager.CreateAsync(ApplicationRole.Admin);


            if (!await _roleManager.RoleExistsAsync(ApplicationRole.User.Name))
                await _roleManager.CreateAsync(ApplicationRole.User);
        }

        private async Task SeedUsers()
        {
            if (await _userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    FirstName = "test ",
                    LastName = "test",
                    Email = "admin@admin.com",
                    UserState = UserState.Active
                };

                var result = await _userManager.CreateAsync(user, "123456");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, ApplicationRole.Admin.Name);
            }
        }
    }

}
