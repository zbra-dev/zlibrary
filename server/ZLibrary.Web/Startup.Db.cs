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
                {
                    return;
                }

                var userFactory =  new UserFactory();
                var users = userFactory.CreateCommomnUsers().Concat(userFactory.CreateAdminUsers());
                context.Users.AddRange(users);

                var authorFactory = new AuthorFactory();
                var authors = authorFactory.CreateAuthors();
                context.Authors.AddRange(authors);

                //Publishers
                var publisherFactory = new PublisherFactory();
                var publishers =  publisherFactory.CreatedPublishers();
                foreach (var publisherItem in publishers)
                {
                    context.Publishers.Add(publisherItem);
                }

                var isbn = new Isbn("12345");
                context.Isbns.Add(isbn);

                var publisher = new Publisher("Cambridge Publisher");
                context.Publishers.Add(publisher);

                var book = new Book()
                {
                    Authors = authors.ToList(),
                    Isbn = isbn,
                    PublicationYear = 2014,
                    Publisher = publisher,
                    Synopsis = "Java for professional development and best practices.",
                    Title = "Effective Java"
                };

                context.Books.Add(book);   

                context.SaveChanges();
            }
        }
    }
}