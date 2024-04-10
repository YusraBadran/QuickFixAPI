using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickFix.DbContexts;
using QuickFix.Identity.Shared.Models;
using QuickFix.Settings.Screens.Data;
using QuickFix.Settings.Screens.Extensions;
using QuickFix.Settings.Screens.Model;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Identitys.Data
{

    public class IdentetyDataSeed
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IScreenContext _screenContext;
        public IdentetyDataSeed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IScreenContext screenContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _screenContext = screenContext;
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
                var screen = services.GetRequiredService<IScreenContext>();
                var seed = new IdentetyDataSeed(userManager, roleManager, screen);
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
                    UserState = TypeStates.Active
                };

                var result = await _userManager.CreateAsync(user, "123456");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, ApplicationRole.Admin.Name);
                    //await SeedScreen();
                }
            }
        }
        private async Task SeedScreen()
        {
            var userId = await _userManager.FindByNameAsync("admin");
            var dashboard = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = null,
                //IconImge = "Upload/menu/icon/dashboard.png",
                Label = "Dashboard",
                order = 1,
                Translate = "menu.home.dashboard",
            };
            await _screenContext.CreateAsync(dashboard);
            var home = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-home",
                Label = "Home",
                order = 2,
                HashName = "home",
                RouterLink = "",
                Translate = "menu.home.title",
                SubId = dashboard.Id
            };
            await _screenContext.CreateAsync(home);
            await AddAddminScreen(userId.Id, home.Id);
            var order = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-book",
                //IconImge = "Upload/menu/icon/order.png",
                Label = "Orders",
                order = 3,
                HashName = "orders",
                RouterLink = "/orders",
                Translate = "menu.orders.title",
                SubId = dashboard.Id
            };
            await _screenContext.CreateAsync(order);
            await AddAddminScreen(userId.Id, order.Id, false);
            var company = new ScreenModel
            {
                Id = Guid.NewGuid(),
                //Icon = "pi pi-fw pi-building",
                Label = "Company",
                order = 4,
                Translate = "menu.company.title",
            };
            await _screenContext.CreateAsync(company);
            var companyList = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-building",
                Label = "Company",
                order = 5,
                HashName = "company",
                RouterLink = "/company",
                Translate = "menu.company.title",
                SubId = company.Id
            };
            await _screenContext.CreateAsync(companyList);
            await AddAddminScreen(userId.Id, companyList.Id);
            var branches = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-sitemap",
                //IconImge = "Upload/menu/icon/branch.png",
                Label = "Branches",
                order = 6,
                HashName = "branches",
                RouterLink = "/branch",
                Translate = "menu.branch.title",
                SubId = company.Id
            };
            await _screenContext.CreateAsync(branches);
            await AddAddminScreen(userId.Id, branches.Id, false);
            var createBranche = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = null,
                Label = "Create Branches",
                order = 7,
                HashName = "createBranches",
                RouterLink = "/branch/create/",
                Translate = "menu.branch.create",
                SubId = branches.Id
            };
            await _screenContext.CreateAsync(createBranche);

            var service = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-truck",
                //IconImge = "Upload/menu/icon/service.png",
                Label = "Service",
                order = 8,
                HashName = "services",
                RouterLink = "/service",
                Translate = "menu.service.title",
                SubId = company.Id
            };
            await _screenContext.CreateAsync(service);
            await AddAddminScreen(userId.Id, service.Id, false);
            var units = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-th-large",
                //IconImge = "Upload/menu/icon/units.png",
                Label = "Units",
                order = 9,
                HashName = "units",
                RouterLink = "/units",
                Translate = "menu.units.title",
                SubId = company.Id
            };
            await _screenContext.CreateAsync(units);
            await AddAddminScreen(userId.Id, units.Id);
            var categories = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-box",
                //IconImge = "Upload/menu/icon/category.svg",
                Label = "Categories",
                order = 10,
                HashName = "categories",
                RouterLink = "/categories",
                Translate = "menu.categories.title",
                SubId = company.Id
            };
            await _screenContext.CreateAsync(categories);
            await AddAddminScreen(userId.Id, categories.Id);
            var setting = new ScreenModel
            {
                Id = Guid.NewGuid(),
                //Icon = "pi pi-wf pi-cog",
                Label = "Setting",
                order = 11,
                Translate = "menu.setting.title",
            };
            await _screenContext.CreateAsync(setting);
            var user = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-wf pi-users",
                Label = "Users",
                order = 12,
                HashName = "users",
                RouterLink = "/users",
                Translate = "menu.setting.users",
                SubId = setting.Id
            };
            await _screenContext.CreateAsync(user);
            await AddAddminScreen(userId.Id, user.Id);
            var createUser = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = null,
                Label = "Create Users",
                order = 13,
                HashName = "createUser",
                RouterLink = "/users/create",
                Translate = "menu.setting.createUser",
                SubId = setting.Id
            };
            await _screenContext.CreateAsync(createUser);
            await AddAddminScreen(userId.Id, createUser.Id, false);
        }
        private async Task AddAddminScreen(Guid UserId, Guid ScreenId, bool menu = true)
        {
            var screenRole = new UserScreen()
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                ScreenId = ScreenId,
                Menu = menu,
                IsCreated = true,
                IsDeleted = true,
                IsDetail = true,
                IsExport = true,
                IsImport = true,
                IsPrint = true,
                IsUpdated = true,
                IsView = true
            };
            var result = await _screenContext.CreateUserScreenAsync(screenRole);
        }
    }

}
