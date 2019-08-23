using System;
using ZLibrary.API;

namespace ZLibrary.Core
{
    public class NoImageService : IImageService
    {
        public void DeleteFile(Guid key)
        {
            
        }

        public byte[] LoadImage(Guid key)
        {
            return new byte[0];
        }

        public Guid SaveImage(Guid key, string filePath)
        {
            return key;
        }
    }
}
