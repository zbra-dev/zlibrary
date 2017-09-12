using System;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public interface IImageRepository
    {
         Task<Guid> SaveFile(byte[] imageData);
    }
}