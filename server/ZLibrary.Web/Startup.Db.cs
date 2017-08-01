using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Web
{
    public partial class Startup
    {
        // TODO: Seed database manually in Production
        private static void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ZLibraryContext>();

                if (context.Users.Any())
                    return;

                var user = new User()
                {
                    Name = "Admin",
                    Email = "admin@fillipe.zone",
                };
                context.Users.Add(user);
                
                context.SaveChanges();
            }
        }
    }
}
