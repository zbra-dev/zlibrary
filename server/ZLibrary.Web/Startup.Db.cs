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
                    Email = "adminZLibrary@zbra.com.br",
                };

                var author = new Author("Joshua Block");

                List<Author> authors = new List<Author>();
                authors.Add(author);
                
                context.Authors.Add(author);

                var isbn = new Isbn("12345");
                context.Isbns.Add(isbn);

                var publisher = new Publisher("Madhouse Inc. ");
                context.Publishers.Add(publisher);

                var book = new Book()
                {
                    Authors = authors,
                    Isbn = isbn,
                    PublicationYear = 2014,
                    Publisher = publisher,
                    Synopsis = "Java for professional development and best practices.",
                    Title = "Effective Java"
                };

                context.Books.Add(book);

                context.Users.Add(user);

                context.SaveChanges();
            }
        }

    }
}