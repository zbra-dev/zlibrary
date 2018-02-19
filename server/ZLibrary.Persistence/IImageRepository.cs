using System;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public interface IImageRepository
    {
        Guid SaveFile(Guid key, byte[] imageData);
        byte[] GetFile(Guid key);
        void DeleteFile(Guid key);
    }
}