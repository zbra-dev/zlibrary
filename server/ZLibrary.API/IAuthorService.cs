using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IAuthorService
    {
        Author FindById(long id);
    }
}