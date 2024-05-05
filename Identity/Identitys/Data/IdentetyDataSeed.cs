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
                    await SeedScreen();
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
            await AddAdminScreen(userId.Id, home.Id);
            var order = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-cart-plus",
                //IconImge = "Upload/menu/icon/order.png",
                Label = "Orders",
                order = 3,
                HashName = "orders",
                RouterLink = "/orders",
                Translate = "menu.orders.title",
                SubId = dashboard.Id
            };
            await _screenContext.CreateAsync(order);
            await AddAdminScreen(userId.Id, order.Id);
            var service = new ScreenModel
            {
                Id = Guid.NewGuid(),
                //Icon = "pi pi-fw pi-building",
                Label = "Services",
                order = 4,
                Translate = "menu.service.title",
            };
            await _screenContext.CreateAsync(service);

            var serviceType = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-hashtag ",
                //IconImge = "Upload/menu/icon/service.png",
                Label = "Service Type",
                order = 5,
                HashName = "servicesType",
                RouterLink = "/services-type",
                Translate = "menu.service.serviceType",
                SubId = service.Id
            };
            await _screenContext.CreateAsync(serviceType);
            await AddAdminScreen(userId.Id, serviceType.Id);
            var categories = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-inbox",
                //IconImge = "Upload/menu/icon/category.svg",
                Label = "Categories",
                order = 6,
                HashName = "categories",
                RouterLink = "/categories",
                Translate = "menu.categories.title",
                SubId = service.Id
            };
            await _screenContext.CreateAsync(categories);
            await AddAdminScreen(userId.Id, categories.Id);
            var categoryItems = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-shopping-cart",
                //IconImge = "Upload/menu/icon/category.svg",
                Label = "category Items",
                order = 7,
                HashName = "categoryItems",
                RouterLink = "/categoriesItem",
                Translate = "menu.categoryItems.title",
                SubId = service.Id
            };
            await _screenContext.CreateAsync(categoryItems);
            await AddAdminScreen(userId.Id, categoryItems.Id);
            var centers = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-fw pi-building",
                //IconImge = "Upload/menu/icon/branch.png",
                Label = "Centers",
                order = 8,
                HashName = "centers",
                RouterLink = "/centers",
                Translate = "menu.centers.title",
                SubId = service.Id
            };
            await _screenContext.CreateAsync(centers);
            await AddAdminScreen(userId.Id, centers.Id);



            var setting = new ScreenModel
            {
                Id = Guid.NewGuid(),
                //Icon = "pi pi-wf pi-cog",
                Label = "Setting",
                order = 9,
                Translate = "menu.setting.title",
            };
            await _screenContext.CreateAsync(setting);
            var user = new ScreenModel
            {
                Id = Guid.NewGuid(),
                Icon = "pi pi-wf pi-user",
                Label = "Users",
                order = 10,
                HashName = "users",
                RouterLink = "/users",
                Translate = "menu.setting.users",
                SubId = setting.Id
            };
            await _screenContext.CreateAsync(user);
            await AddAdminScreen(userId.Id, user.Id);

        }
        private async Task AddAdminScreen(Guid UserId, Guid ScreenId, bool menu = true)
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
