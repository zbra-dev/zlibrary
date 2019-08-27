using System;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Validators
{
    public class BookValidator : IValidator<BookDto>
    {
        private readonly ValidationContext context;
        private readonly IsbnValidator isbnValidator;

        public BookValidator(ValidationContext context, IsbnValidator isbnValidator)
        {
            this.context = context;
            this.isbnValidator = isbnValidator;
        }

        public ValidationResult Validate(BookDto value)
        {
            var validationResult = new ValidationResult();

            if (value == null)
            {
                validationResult.ErrorMessage = "Dados do livro inválidos.";
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(value.Title))
            {
                validationResult.ErrorMessage = "Título não pode estar em branco.";
                return validationResult;
            }

            var isbn = isbnValidator.Validate(value.Isbn);
            if (isbn.HasError)
            {
                validationResult.ErrorMessage = isbn.ErrorMessage;
                return validationResult;
            }
            validationResult.AddResult(isbn);

            if (value.PublicationYear < 0 || value.PublicationYear > DateTime.Today.Year)
            {
                validationResult.ErrorMessage = "Ano de publicação inválido.";
                return validationResult;
            }

            var publisherId = value.Publisher.Id;

            var publisher = context.PublisherService.FindById(publisherId);

            if (publisher == null)
            {
                validationResult.ErrorMessage = "Editora não foi encontrada.";
                return validationResult;
            }

            var authorIds = value.Authors.Select(d => d.Id).ToArray();
            var authorList = new List<BookAuthor>(authorIds.Length);

            foreach (var authorId in authorIds.Where(id => id.HasValue))
            {
                var author = context.AuthorService.FindById(authorId.Value);

                if (author == null)
                {
                    validationResult.ErrorMessage = "Autor não foi encontrado.";
                    return validationResult;
                }
                var bookAuthor = new BookAuthor()
                {
                    Author = author,
                    AuthorId = author.Id,
                };
                authorList.Add(bookAuthor);
            }

            validationResult.AddResult(publisher);
            validationResult.AddResult(authorList);

            return validationResult;
        }
    }
}