using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Victor", 
                        UserName = "victor",
                        Email = "victor@test.com",
                        Phone = "9358234245",
                        Type = "Student",
                    },
                    new AppUser
                    {
                        DisplayName = "Diana", 
                        UserName = "diana",
                        Email = "diana@test.com",
                        Phone = "9137532580",
                        Type = "Student",
                    },
                    new AppUser
                    {
                        DisplayName = "Sneha", 
                        UserName = "sneha",
                        Email = "sneha@test.com",
                        Phone = "9137532888",
                        Type = "Student",
                    }

                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");        
                }
            }

        }
    }
}