namespace ZLibrary.Web.LookUps
{
    public interface IValidationResultDataLookUp
    {
         R LookUp<R>() where R : class;
    }
}