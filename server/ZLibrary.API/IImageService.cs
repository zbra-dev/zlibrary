using System;
using System.Threading.Tasks;

namespace ZLibrary.API
{
    public interface IImageService
    {
        Task<Guid> SaveImage(Guid key, byte[] fileData);
        Task<byte[]> LoadImage(Guid key);
        void DeleteFile(Guid key);
    }
}