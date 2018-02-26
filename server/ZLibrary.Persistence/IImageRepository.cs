using System;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public interface IImageRepository
    {
        Guid SaveFile(Guid key, string filePath);
        byte[] GetFile(Guid key);
        void DeleteFile(Guid key);
    }
}