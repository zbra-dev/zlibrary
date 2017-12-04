using Microsoft.AspNetCore.Http;

namespace ZLibrary.Web.Validators
{
    public class ImageValidator
    {
        public ImageValidator()
        {
        }

        public ValidationResult Validate(IFormFile file)
        {
            var validationResult = new ValidationResult();

            if (file.ContentType != "image/png")
            {
                validationResult.ErrorMessage = "Formato de arquivo inválido, tente uma imagem com extessão '.png'.";
                return validationResult;
            }

            return validationResult;
        }
    }
}