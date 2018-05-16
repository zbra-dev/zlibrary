using ZLibrary.Web.Validators;

namespace ZLibrary.Web.LookUps
{
    public class DefaultValidationResultDataLookUp : IValidationResultDataLookUp
    {
        private readonly ValidationResult validationResult;

        public DefaultValidationResultDataLookUp(ValidationResult validationResult)
        {
            this.validationResult = validationResult;
        }

        public R LookUp<R>() where R : class
        {
            return validationResult.GetResult<R>();
        }
    }
}