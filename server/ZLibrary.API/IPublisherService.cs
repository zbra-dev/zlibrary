using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IPublisherService
    {
        Publisher FindById(long id);
    }
}