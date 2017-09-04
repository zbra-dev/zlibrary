using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IPublisherService
    {
        Task<IList<Publisher>> FindAll();
        Task<Publisher> FindById(long id);
        Task Delete(long id);
        Task<long> Create(Publisher publisher);
        Task<Publisher> FindByName(string keyword);
    }
}