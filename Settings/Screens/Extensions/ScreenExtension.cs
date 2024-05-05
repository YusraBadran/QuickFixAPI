using QuickFix.Settings.Menus.Model;
using QuickFix.Shared.Module;
using Microsoft.EntityFrameworkCore;
using QuickFix.Settings.Screens.Data;
using QuickFix.Settings.Screens.Model;

namespace QuickFix.Settings.Screens.Extensions
{
    public static class ScreenExtension
    {
        /// <summary>
        /// Finds all screen.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static async Task<List<ScreenModel>> FindAllScreen(this IScreenContext context)
        {
            return await context.screens.OrderBy(x => x.order).ToListAsync();
        }
        /// <summary>
        /// Gets the user menu.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public static async Task<List<ScreenModel>> GetUserMenu(this IScreenContext context, Guid Id)
        {
            return await context.screens.Where(s => s.SubId == null || s.Id == s.Role.FirstOrDefault(r => r.UserId == Id && r.Menu == true).ScreenId).OrderBy(x => x.order).ToListAsync();
        }
        /// <summary>
        /// Finds the user screen by identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public static async Task<UserScreen> FindUserScreenById(this IScreenContext context, Guid Id)
        {

            return await context.userScreens.FirstOrDefaultAsync(x => x.Id == Id);
        }
        /// <summary>
        /// Finds All the user screens by identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public static async Task<List<UserScreen>> FindAllUserScreensById(this IScreenContext context, Guid Id)
        {

            return await context.userScreens.Where(x => x.UserId == Id).ToListAsync();
        }
        /// <summary>
        /// Updates the permissions asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        public static async Task<DataRespons> UpdatePermissionsAsync(this IScreenContext context, UserScreen screen)
        {

            try
            {
                context.userScreens.Update(screen);
                await context.SaveChangesAsync();
                var success = new DataRespons()
                {
                    Id = screen.Id,
                    StatusCode = 200,

                };
                return success;
            }
            catch (Exception ex)
            {
                var field = new DataRespons()
                {
                    StatusCode = 400,
                    Message = ex.Message,

                };
                return field;
            }

        }

        /// <summary>
        /// Finds screen by hasName
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="hashName">Name of the hash.</param>
        /// <returns></returns>
        public static async Task<ScreenModel> FindScreenByHashName(this IScreenContext context, string hashName)
        {

            return await context.screens.FirstOrDefaultAsync(x => x.HashName == hashName);
        }
        /// <summary>
        /// Creates the Screen.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        public static async Task<DataRespons> CreateAsync(this IScreenContext context, ScreenModel screen)
        {

            try
            {
                await context.screens.AddAsync(screen);
                await context.SaveChangesAsync();
                var success = new DataRespons()
                {
                    Id = screen.Id,
                    StatusCode = 200,

                };
                return success;
            }
            catch (Exception ex)
            {
                var field = new DataRespons()
                {
                    StatusCode = 400,
                    Message = ex.Message,

                };
                return field;
            }

        }
        public static async Task<DataRespons> UpdateAsync(this IScreenContext context, ScreenModel screen)
        {

            try
            {
                context.screens.Update(screen);
                await context.SaveChangesAsync();
                var success = new DataRespons()
                {
                    Id = screen.Id,
                    StatusCode = 200,

                };
                return success;
            }
            catch (Exception ex)
            {
                var field = new DataRespons()
                {
                    StatusCode = 400,
                    Message = ex.Message,

                };
                return field;
            }

        }
        /// <summary>
        /// Creates the user screen Menu.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        public static async Task<DataRespons> CreateUserScreenAsync(this IScreenContext context, UserScreen screen)
        {

            try
            {
                await context.userScreens.AddAsync(screen);
                await context.SaveChangesAsync();
                var success = new DataRespons()
                {
                    Id = screen.Id,
                    StatusCode = 200,

                };
                return success;
            }
            catch (Exception ex)
            {
                var field = new DataRespons()
                {
                    StatusCode = 400,
                    Message = ex.Message,

                };
                return field;
            }

        }
        /// <summary>
        /// Deletes the user screen asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteUserScreenAsync(this IScreenContext context, UserScreen screen)
        {

            try
            {
                context.userScreens.Remove(screen);
                await context.SaveChangesAsync();
                var success = new DataRespons()
                {
                    Id = screen.Id,
                    StatusCode = 200,

                };
                return success;
            }
            catch (Exception ex)
            {
                var field = new DataRespons()
                {
                    StatusCode = 400,
                    Message = ex.Message,

                };
                return field;
            }

        }
        /// <summary>
        /// Deletes all user screens asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAllUserScreensAsync(this IScreenContext context, List<UserScreen> screen)
        {

            try
            {
                context.userScreens.RemoveRange(screen);
                await context.SaveChangesAsync();
                var success = new DataRespons()
                {
                    StatusCode = 200,

                };
                return success;
            }
            catch (Exception ex)
            {
                var field = new DataRespons()
                {
                    StatusCode = 400,
                    Message = ex.Message,

                };
                return field;
            }

        }
    }
}
