namespace GrowATree.Infrastructure.Persistence
{
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Domain.Entities;
    using Microsoft.AspNetCore.Identity;

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<User> userManager)
        {
            var defaultUser = new User { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var storeRole = new IdentityRole
            {
                Name = Constants.StoreRoleName,
            };

            if (await roleManager.FindByNameAsync(Constants.StoreRoleName) == null)
            {
                await roleManager.CreateAsync(storeRole);
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
        }
    }
}
