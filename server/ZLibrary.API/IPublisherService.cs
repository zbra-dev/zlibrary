using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IPublisherService
    {
        Task<IList<Publisher>> FindAll();
        Publisher FindById(long id);
    }
}