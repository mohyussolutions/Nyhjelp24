
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Seeding
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.Users.Add(new User { Name = "Admin" });
                context.SaveChanges();
            }
        }
    }
}
