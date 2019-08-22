using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Validators
{
    public class AuthorDtoValidator : IValidator<AuthorDto>
    {
        public ValidationResult Validate(AuthorDto value)
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
