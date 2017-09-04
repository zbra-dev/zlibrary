using System.Collections.Generic;
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

            var publisherId = value.PublisherId;

            var publisher = context.PublisherService.FindById(publisherId);
            
            if (publisher == null) 
            {
                validationResult.ErrorMessage = "Editora não foi encontrada";
                return validationResult;
            }

            var authorIds = value.AuthorIds;
            var authorList = new List<Author>(authorIds.Length);

            foreach (var authorId in authorIds)
            {
                var author = context.AuthorService.FindById(authorId);

                if (author == null)
                {
                    validationResult.ErrorMessage = "Autor não foi encontrado";
                    return validationResult;
                }

                authorList.Add(author);
            }

            validationResult.AddResult(publisher);
            validationResult.AddResult(authorList);

            return validationResult;
        }
    }
}