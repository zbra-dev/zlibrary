using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Persistence;
using System.Collections.Generic;
using System;

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

                context.SaveChanges();

                /***********************************/
                /**** ONLY FOR TESTS PURPOSES   ****/
                /***********************************/


                var andrewTroelsen = context.Authors.Single(a => a.Name == "Andrew Troelsen");
                var jackPhillips = context.Authors.Single(a => a.Name == "Jack Phillips");
                var simonSarris = context.Authors.Single(a => a.Name == "Simon Sarris");
                var martinFowler = context.Authors.Single(a => a.Name == "Martin Fowler");

                var book = new Book()
                {
                    Isbn = Isbn.FromValue("9780321127426"),
                    PublicationYear = 2014,
                    Publisher = publishers.Where<Publisher>(p => p.Name == "Editora Futura").SingleOrDefault(),
                    Synopsis = "HTML5 Unleashed is the authoritative guide that covers the key web components driving the future of the Web",
                    Title = "HTML 5 - Unleashed"
                };
                context.Books.Add(book);
                context.SaveChanges();

                book.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book,
                        BookId = book.Id,
                        Author = simonSarris,
                        AuthorId = simonSarris.Id
                    }
                };
                context.SaveChanges();

                var book2 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java"
                };
                context.Books.Add(book2);
                context.SaveChanges();

                book2.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book2,
                        BookId = book2.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book2,
                        BookId = book2.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book3 = new Book()
                {
                    Isbn = Isbn.FromValue("9780316037723"),
                    PublicationYear = 2014,
                    Publisher = publishers.Where<Publisher>(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                    Synopsis = "The practice of enterprise application development has benefited from the emergence of many new enabling technologies.",
                    Title = "Enterprise Application Architecture"
                };

                context.Books.Add(book3);
                context.SaveChanges();

                book3.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book3,
                        BookId = book3.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book3,
                        BookId = book3.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                /*                
                                var book3 = new Book()
                                {
                                    Authors = new List<Author>() { martinFowler },
                                    Isbn = Isbn.FromValue("9780316037723"),
                                    PublicationYear = 2014,
                                    Publisher = publishers.Where<Publisher>(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                                    Synopsis = "The practice of enterprise application development has benefited from the emergence of many new enabling technologies.",
                                    Title = "Patterns of Enterprise Application Architecture"
                                };
                                martinFowler.Books = new List<Book>() { book3 };
                                context.Books.Add(book3);
                                context.SaveChanges();

                                var book4 = new Book()
                                {
                                    Authors = new List<Author>() { andrewTroelsen },
                                    Isbn = Isbn.FromValue("9780201738292"),
                                    PublicationYear = 2014,
                                    Publisher = publishers.FirstOrDefault(),
                                    Synopsis = "Java 2 for professional development and best practices.",
                                    Title = "Java"
                                };
                                andrewTroelsen.Books = new List<Book>() { book2, book4 };
                                context.Books.Add(book4);
                                context.SaveChanges();

                                var book5 = new Book()
                                {
                                    Authors = context.Authors.Where(a => a.Name == "Andrew Troelsen" ||  a.Name == "Martin Fowler").ToList(),
                                    Isbn = Isbn.FromValue("9780316380508"),
                                    PublicationYear = 2014,
                                    Publisher = publishers.FirstOrDefault(),
                                    Synopsis = "Java 2 for professional development and best practices.",
                                    Title = "Java Cool"
                                };
                                context.Books.Add(book5);
                                context.SaveChanges();

                                var book6 = new Book()
                                {
                                    Authors = context.Authors.Where(a => a.Name == "Andrew Troelsen" ||  a.Name == "Martin Fowler").ToList(),
                                    Isbn = Isbn.FromValue("9780764542800"),
                                    PublicationYear = 2014,
                                    Publisher = publishers.Where(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                                    Synopsis = "Java 2 for professional development and best practices.",
                                    Title = "Java Fuck Cool"
                                };
                                context.Books.Add(book6);
                                context.SaveChanges();

                                var book7 = new Book()
                                {
                                    Authors = new List<Author>() { context.Authors.Single(a => a.Name == "Andrew Troelsen") },
                                    Isbn = Isbn.FromValue("9780764542800"),
                                    PublicationYear = 2014,
                                    Publisher = context.Publishers.Where(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                                    Synopsis = "Java 2 for professional development and best practices.",
                                    Title = "Java Awesome Cool"
                                };
                                context.Books.Add(book7);
                                context.SaveChanges();
                 */
                var bookId = 1;
                var firstUser = users.First();

                var reservation1 = new Reservation(bookId, firstUser);
                reservation1.Reason.Description = "Solicitação de Teste1";
                context.Reservations.Add(reservation1);
                context.SaveChanges();

                var loan1 = new Loan(reservation1);
                context.Loans.Add(loan1);
                context.SaveChanges();
            }
        }
    }
}