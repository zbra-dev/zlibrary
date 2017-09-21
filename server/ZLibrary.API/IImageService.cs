using System;
using System.Threading.Tasks;

namespace ZLibrary.API
{
    public interface IImageService
    {
         Task<Guid> SaveImage(byte[] fileData, string contentType);
    }
}