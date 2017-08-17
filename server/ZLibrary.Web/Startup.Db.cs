using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Persistence;
using System.Collections.Generic;

namespace ZLibrary.Web
{
    public partial class Startup
    {
        private static void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ZLibraryContext>();

                if (context.Users.Any())
                {
                    return;
                }

                var users = UserFactory.CreateUsers();
                context.Users.AddRange(users);

                var authors = AuthorFactory.CreateAuthors();
                context.Authors.AddRange(authors);

                var publishers = PublisherFactory.CreatePublishers();
                context.Publishers.AddRange(publishers);

                var book = new Book()
                {
                    Authors = authors.Where(a => a.Name == "Jack Phillips" || a.Name == "Andrew Troelsen").ToList(),
                    Isbn = Isbn.FromValue("9780201738292"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java for professional development and best practices.",
                    Title = "Effective Java"
                };

                context.Books.Add(book);

                context.SaveChanges();
            }
        }
    }
}