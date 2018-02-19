using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IPublisherRepository
    {
        Task<IList<Publisher>> FindAll();
        Publisher FindById(long id);
        Task<IList<Publisher>> FindByName(string name);
    }
}