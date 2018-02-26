using Microsoft.AspNetCore.Http;

namespace ZLibrary.Web.Validators
{
    public class ImageValidator
    {
        public ImageValidator()
        {
        }

        public ValidationResult Validate(string fileName)
        {
            var validationResult = new ValidationResult();

            if (!fileName.Contains(".png"))
            {
                validationResult.ErrorMessage = "Formato de arquivo inválido, tente uma imagem com extessão '.png'.";
                return validationResult;
            }

            return validationResult;
        }
    }
}