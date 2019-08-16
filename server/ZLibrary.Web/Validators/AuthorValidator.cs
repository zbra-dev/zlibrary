using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Validators
{
    public class AuthorDTOValidator : IValidator<AuthorDTO>
    {
        public ValidationResult Validate(AuthorDTO value)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrEmpty(value.Name))
            {
                validationResult.ErrorMessage = "Nome é obrigatório.";
                return validationResult;
            }

            return validationResult;
        }
    }
}
