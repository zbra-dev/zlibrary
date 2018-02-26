using System;
using System.Threading.Tasks;

namespace ZLibrary.API
{
    public interface IImageService
    {
        Guid SaveImage(Guid key, string filePath);
        byte[] LoadImage(Guid key);
        void DeleteFile(Guid key);
    }
}