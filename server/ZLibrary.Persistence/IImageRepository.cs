using System;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public interface IImageRepository
    {
        Task<Guid> SaveFile(Guid key, byte[] imageData);
        Task<byte[]> GetFile(Guid key);
        void DeleteFile(Guid key);
    }
}