using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Validators
{
    public class SlackAuthenticationDataValidator : IValidator<SlackUserDto>
    {
        
        public ValidationResult Validate(SlackUserDto value)
        {
            var validationResult = new ValidationResult();

            if (value == null)
            {
                validationResult.ErrorMessage = "Dados inválidos.";
            }
            else
            {
                if (value.User == null)
                {
                    validationResult.ErrorMessage =  "Informações do usuário inválidas";
                }
                else
                {
                    var userEmail = value.User.Email;
                    if (string.IsNullOrWhiteSpace(userEmail))
                    {
                        validationResult.ErrorMessage =  "Email é obrigatório";
                    }
                }
            }

            return validationResult;
        }
    }
}