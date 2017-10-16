namespace ZLibrary.Web.Controllers
{
    public interface IAuthenticationApi
    {
        string SlackApiBase {get;}
        string ClientId {get;}
        string ClientSecret {get;}
        string GetAuthenticationUrl(string code);
    }
}