using System;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Validators
{
    public class BookValidator : IValidator<BookDTO>
    {
        private readonly ValidationContext context;

        public BookValidator(ValidationContext context)
        {
            this.context = context;
        }

        public ValidationResult Validate(BookDTO value)
        {
            var validationResult = new ValidationResult();

            if (value == null)
            {
                validationResult.ErrorMessage = "Dados inválidos.";
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(value.Title))
            {
                validationResult.ErrorMessage = "Título não pode estar em branco.";
                return validationResult;
            }

            try
            {
                var isbn = Isbn.FromValue(value.Isbn);
                validationResult.AddResult(isbn);
            }
            catch (IsbnException ex)
            {
                validationResult.ErrorMessage = ex.Message;
                return validationResult;
            }

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

            foreach (var authorId in authorIds)
            {
                var author = context.AuthorService.FindById(authorId);

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