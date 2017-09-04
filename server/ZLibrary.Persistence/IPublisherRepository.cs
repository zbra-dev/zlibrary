using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IPublisherRepository
    {
        Task<IList<Publisher>> FindAll();
        Task<Publisher> FindById(long id);
        Task<Publisher> FindByName(string name);
        Task Delete(long id);
        Task<long> Create(Publisher publisher);
    }
}