namespace GrowATree.Infrastructure.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Domain.Entities;
    using Microsoft.AspNetCore.Identity;

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUsersAsync(UserManager<User> userManager)
        {
            var defaultUser = new User { UserName = "user1", Email = "user1@trees.com", City = "Благоевград", EmailConfirmed = true, };
            var defaultUser1 = new User { UserName = "user2", Email = "user2@trees.com", City = "Благоевград", EmailConfirmed = true, };
            var defaultUser2 = new User { UserName = "user3", Email = "user3@trees.com", City = "Благоевград", EmailConfirmed = true, };

            if (!userManager.Users.Any(x => x.UserName == "user1"))
            {
                var result = await userManager.CreateAsync(defaultUser, "user1");
                await userManager.CreateAsync(defaultUser1, "user2");
                await userManager.CreateAsync(defaultUser2, "user3");
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

        public static async Task SeedDefaultStoresAsync(UserManager<User> userManager, ApplicationDbContext context)
        {
            var storeUser = new User { UserName = "storeUser1", Email = "storeUser1@trees.com", City = "Благоевград", PhoneNumber = "0898700133", EmailConfirmed = true, };
            var storeUser1 = new User { UserName = "storeUser2", Email = "storeUser2@trees.com", City = "Благоевград", PhoneNumber = "0898700134", EmailConfirmed = true, };
            var storeUser2 = new User { UserName = "storeUser3", Email = "storeUser3@trees.com", City = "Благоевград", PhoneNumber = "0898700135", EmailConfirmed = true, };

            if (!userManager.Users.Any(x => x.UserName == "storeUser1"))
            {
                await userManager.CreateAsync(storeUser, "storeUser1");
                await userManager.CreateAsync(storeUser1, "storeUser2");
                await userManager.CreateAsync(storeUser2, "storeUser3");

                await userManager.AddToRoleAsync(storeUser, Constants.StoreRoleName);
                await userManager.AddToRoleAsync(storeUser1, Constants.StoreRoleName);
                await userManager.AddToRoleAsync(storeUser2, Constants.StoreRoleName);
            }

            var store1 = new Store { Latitude = "23.1412412", Longitude = "24.21154125", Description = "Sore 1 desc", WorkingHours = "Store 1 raboti ot 8 do 6", ApplicationUser = storeUser, ApplicationUserId = storeUser.Id };
            var store2 = new Store { Latitude = "23.1412412", Longitude = "24.21154125", Description = "Sore 2 desc", WorkingHours = "Store 2 raboti ot 8 do 6", ApplicationUser = storeUser1, ApplicationUserId = storeUser1.Id };
            var store3 = new Store { Latitude = "23.1412412", Longitude = "24.21154125", Description = "Sore 3 desc", WorkingHours = "Store 3 raboti ot 8 do 6", ApplicationUser = storeUser2, ApplicationUserId = storeUser2.Id };

            if (!context.Stores.Any(x => x.Description == "Sore 1 desc"))
            {
                await context.Stores.AddAsync(store1);
                await context.Stores.AddAsync(store2);
                await context.Stores.AddAsync(store3);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
        }
    }
}
