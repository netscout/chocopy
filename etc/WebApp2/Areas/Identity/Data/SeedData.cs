using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Authorization;
using WebApp2.Data;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -udl -outDir Pages\Contacts --referenceScriptLibraries

namespace WebApp2.Areas.Identity.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new WebApp2IdentityDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<WebApp2IdentityDbContext>>()))
            {   
                var adminID = EnsureUser(serviceProvider, testUserPw, "admin@admin.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                var managerID = EnsureUser(serviceProvider, testUserPw, "manager@admin.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                SeedDB(context, adminID);
            }
        }
        
        private static async Task<string>EnsureUser(IServiceProvider serviceProvider,
            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<WebApp2User>>();

            var user = await userManager.FindByNameAsync(UserName);
            if(user == null)
            {
                user = new WebApp2User { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
            string uid, string role)
        {
            IdentityResult ir = null;
            var roleManager = serviceProvider.GetService<RoleManager<WebApp2Role>>();

            if(roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if(!await roleManager.RoleExistsAsync(role))
            {
                ir = await roleManager.CreateAsync(new WebApp2Role(role));
            }

            var userManager = serviceProvider.GetService<UserManager<WebApp2User>>();

            var user = await userManager.FindByIdAsync(uid);

            ir = await userManager.AddToRoleAsync(user, role);

            return ir;
        }

        public static void SeedDB(WebApp2IdentityDbContext context, string adminID)
        {
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }

            context.Contact.AddRange(
                new Contact
                {
                    Name = "Debra Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "debra@example.com",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Thorsten Weinrich",
                    Address = "5678 1st Ave W",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "thorsten@example.com",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Yuhong Li",
                    Address = "9012 State st",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "yuhong@example.com",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Jon Orton",
                    Address = "3456 Maple St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "jon@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Diliana Alexieva-Bosseva",
                    Address = "7890 2nd Ave E",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "diliana@example.com",
                    Status = ContactStatus.Rejected,
                    OwnerID = adminID
                }
             );
            context.SaveChanges();
        }
    }
}