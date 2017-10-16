namespace ZLibrary.Web.Controllers
{
    public class SlackApi : IAuthenticationApi
    {
        public string SlackApiBase {get { return "https://slack.com/api/{0}"; } }
        public string ClientId {get {return "6575166742.236473063552";}}
        public string ClientSecret  {get {return "e33f0fbba6c17d24ecae452a9f146b8c";}}

        public string GetAuthenticationUrl(string code) 
        {
            return string.Format(SlackApiBase, "oauth.access") + "?" + BuildQueryString(code);
        }
        
        private string BuildQueryString(string code)
        {
            return $"client_id={ClientId}&client_secret={ClientSecret}&code={code}";
        }
    }
}