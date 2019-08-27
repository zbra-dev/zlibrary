using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Validators
{
    public class PublisherDtoValidator : IValidator<PublisherDto>
    {
        public ValidationResult Validate(PublisherDto value)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrEmpty(value.Name))
            {
                validationResult.ErrorMessage = "Nome é obrigatório";
                return validationResult;
            }

            return validationResult;
        }
    }
}
