using ZLibrary.Model;
using ZLibrary.Web.Resources;

namespace ZLibrary.Web.Factory
{
    public interface ITokenFactory
    {
        TokenResource CreateToken(User user);
    }
}
