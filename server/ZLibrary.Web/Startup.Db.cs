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
                 var bruceEckel = context.Authors.Single(a => a.Name == "Bruce Eckel");
                

                var book = new Book()
                {
                    Isbn = Isbn.FromValue("9780321127426"),
                    PublicationYear = 2014,
                    Publisher = publishers.Where<Publisher>(p => p.Name == "Editora Futura").SingleOrDefault(),
                    Synopsis = "HTML5 Unleashed is the authoritative guide that covers the key web components driving the future of the Web",
                    Title = "HTML 5 - Unleashed",
                    NumberOfCopies = 3,
                    CoverImageKey = Guid.Parse("80ae1455-8f43-4a40-aa41-39e486fd61d1")
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
                    Title = "Effective Java Java Java Java Java Java Java Java Java Java Java Java Java Java Java Javaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a21")
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
                    },
                    new BookAuthor()
                    {
                        Book = book2,
                        BookId = book2.Id,
                        Author = bruceEckel,
                        AuthorId = bruceEckel.Id
                    }
                };
                context.SaveChanges();

                var book3 = new Book()
                {
                    Isbn = Isbn.FromValue("9780316037723"),
                    PublicationYear = 2014,
                    Publisher = publishers.Where<Publisher>(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                    Synopsis = "The practice of enterprise application development has benefited from the emergence of many new enabling technologies.",
                    Title = "Enterprise Application Architecture",
                    NumberOfCopies = 1,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a22")
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

                   var book4 = new Book()
                {
                    Isbn = Isbn.FromValue("9780316037723"),
                    PublicationYear = 2014,
                    Publisher = publishers.Where<Publisher>(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                    Synopsis = "The practice of enterprise application development has benefited from the emergence of many new enabling technologies.",
                    Title = "Enterprise Application Architecture",
                    NumberOfCopies = 1,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a23")
                };

                context.Books.Add(book4);
                context.SaveChanges();

                book4.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book4,
                        BookId = book4.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book4,
                        BookId = book4.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                   var book5 = new Book()
                {
                    Isbn = Isbn.FromValue("9780316037723"),
                    PublicationYear = 2014,
                    Publisher = publishers.Where<Publisher>(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                    Synopsis = "The practice of enterprise application development has benefited from the emergence of many new enabling technologies.",
                    Title = "Enterprise Application Architecture",
                    NumberOfCopies = 1,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a24")
                };

                context.Books.Add(book5);
                context.SaveChanges();

                book5.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book5,
                        BookId = book5.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book5,
                        BookId = book5.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                 var book6 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a25")
                };
                context.Books.Add(book6);
                context.SaveChanges();

                book6.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book6,
                        BookId = book6.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book6,
                        BookId = book6.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book7 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a26")
                };
                context.Books.Add(book7);
                context.SaveChanges();

                book7.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book7,
                        BookId = book7.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book7,
                        BookId = book7.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book8 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a27")
                };
                context.Books.Add(book8);
                context.SaveChanges();

                book8.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book8,
                        BookId = book8.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book8,
                        BookId = book8.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book9 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a28")
                };
                context.Books.Add(book9);
                context.SaveChanges();

                book9.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book9,
                        BookId = book9.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book9,
                        BookId = book9.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book10 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a29")
                };
                context.Books.Add(book10);
                context.SaveChanges();

                book10.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book10,
                        BookId = book10.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book10,
                        BookId = book10.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book11 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a30")
                };
                context.Books.Add(book11);
                context.SaveChanges();

                book11.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book11,
                        BookId = book11.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book11,
                        BookId = book11.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();

                var book12 = new Book()
                {
                    Isbn = Isbn.FromValue("9780672336270"),
                    PublicationYear = 2014,
                    Publisher = publishers.FirstOrDefault(),
                    Synopsis = "Java 2 for professional development and best practices.",
                    Title = "Effective Java",
                    NumberOfCopies = 2,
                    CoverImageKey = Guid.Parse("5b0cf369-a0b2-4643-ac84-36058e729a31")
                };
                context.Books.Add(book12);
                context.SaveChanges();

                book12.Authors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book12,
                        BookId = book12.Id,
                        Author = jackPhillips,
                        AuthorId = jackPhillips.Id
                    },
                    new BookAuthor()
                    {
                        Book = book12,
                        BookId = book12.Id,
                        Author = andrewTroelsen,
                        AuthorId = andrewTroelsen.Id
                    }
                };
                context.SaveChanges();
                
                // var book3 = new Book()
                // {
                //     Authors = new List<Author>() { martinFowler },
                //     Isbn = Isbn.FromValue("9780316037723"),
                //     PublicationYear = 2014,
                //     Publisher = publishers.Where<Publisher>(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                //     Synopsis = "The practice of enterprise application development has benefited from the emergence of many new enabling technologies.",
                //     Title = "Patterns of Enterprise Application Architecture"
                // };
                // martinFowler.Books = new List<Book>() { book3 };
                // context.Books.Add(book3);
                // context.SaveChanges();

                // var book4 = new Book()
                // {
                //     Authors = new List<Author>() { andrewTroelsen },
                //     Isbn = Isbn.FromValue("9780201738292"),
                //     PublicationYear = 2014,
                //     Publisher = publishers.FirstOrDefault(),
                //     Synopsis = "Java 2 for professional development and best practices.",
                //     Title = "Java"
                // };
                // andrewTroelsen.Books = new List<Book>() { book2, book4 };
                // context.Books.Add(book4);
                // context.SaveChanges();

                // var book5 = new Book()
                // {
                //     Authors = context.Authors.Where(a => a.Name == "Andrew Troelsen" || a.Name == "Martin Fowler").ToList(),
                //     Isbn = Isbn.FromValue("9780316380508"),
                //     PublicationYear = 2014,
                //     Publisher = publishers.FirstOrDefault(),
                //     Synopsis = "Java 2 for professional development and best practices.",
                //     Title = "Java Cool"
                // };
                // context.Books.Add(book5);
                // context.SaveChanges();

                // var book6 = new Book()
                // {
                //     Authors = context.Authors.Where(a => a.Name == "Andrew Troelsen" || a.Name == "Martin Fowler").ToList(),
                //     Isbn = Isbn.FromValue("9780764542800"),
                //     PublicationYear = 2014,
                //     Publisher = publishers.Where(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                //     Synopsis = "Java 2 for professional development and best practices.",
                //     Title = "Java Fuck Cool"
                // };
                // context.Books.Add(book6);
                // context.SaveChanges();

                // var book7 = new Book()
                // {
                //     Authors = new List<Author>() { context.Authors.Single(a => a.Name == "Andrew Troelsen") },
                //     Isbn = Isbn.FromValue("9780764542800"),
                //     PublicationYear = 2014,
                //     Publisher = context.Publishers.Where(p => p.Name == "Addison - Wesley").SingleOrDefault(),
                //     Synopsis = "Java 2 for professional development and best practices.",
                //     Title = "Java Awesome Cool"
                // };
                // context.Books.Add(book7);
                // context.SaveChanges();
                
                var bookId = 1;
                var firstUser = users.SingleOrDefault(u => u.Id == 17);
                var secondUser = users.SingleOrDefault(u => u.Id == 16);

                var reservation1 = new Reservation(bookId, firstUser);
                reservation1.Reason.Status = ReservationStatus.Requested;
                context.Reservations.Add(reservation1);
                context.SaveChanges();

                var reservation2 = new Reservation(2, firstUser);
                reservation2.Reason.Status = ReservationStatus.Requested;
                context.Reservations.Add(reservation2);
                context.SaveChanges();

                var reservation3 = new Reservation(3, firstUser);
                reservation3.Reason.Status = ReservationStatus.Waiting;
                context.Reservations.Add(reservation3);
                context.SaveChanges();

                var reservation4 = new Reservation(3, firstUser);
                reservation4.Reason.Status = ReservationStatus.Approved;
                context.Reservations.Add(reservation4);
                context.SaveChanges();

                var loan1 = new Loan(reservation1);
                loan1.Status = LoanStatus.Borrowed;
                //loan1.ExpirationDate = DateTime.Now.AddDays(-1);
                context.Loans.Add(loan1);
                context.SaveChanges();

                var loan2 = new Loan(reservation2);
                loan2.Status = LoanStatus.Returned;
                context.Loans.Add(loan2);
                context.SaveChanges();

                 var loan3 = new Loan(reservation3);
                 context.Loans.Add(loan3);
                 context.SaveChanges();
                 
                 var loan4 = new Loan(reservation4);
                 context.Loans.Add(loan4);
                 context.SaveChanges();
            }
        }
    }
}