using ChatRoomApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoomApp.Data
{
    public static class DbSeeder
    {
        public static void SeedDb(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();
            CreateRoles(roleManager);
            CreateUserAdmin(userManager);
        }

        private static void CreateUserAdmin(UserManager<User> userManager)
        {
            var adminUser = userManager.FindByEmailAsync("superadmin@mail.com").Result;

            if (adminUser == null)
            {
                adminUser = new User()
                {
                    FirstName = "Super Admin",
                    Email = "superadmin@mail.com",
                    UserName = "superadmin"
                };

                userManager.CreateAsync(adminUser, "Superadmin.123").Wait();
            }

            if (!userManager.IsInRoleAsync(adminUser, RolesList.Admin).Result)
            {
                userManager.AddToRoleAsync(adminUser, RolesList.Admin).Wait();
            }

            if (!userManager.IsInRoleAsync(adminUser, RolesList.ChatUser).Result)
            {
                userManager.AddToRoleAsync(adminUser, RolesList.ChatUser).Wait();
            }
        }

        private static void CreateRoles(RoleManager<Role> roleManager)
        {
            if (roleManager.FindByNameAsync(RolesList.Admin).Result == null)
            {
                roleManager.CreateAsync(new Role() { Name = RolesList.Admin }).Wait();
            }

            if (roleManager.FindByNameAsync(RolesList.ChatUser).Result == null)
            {
                roleManager.CreateAsync(new Role() { Name = RolesList.ChatUser }).Wait();
            }
        }
    }
}
