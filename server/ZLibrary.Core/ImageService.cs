using System;
using System.IO;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public Guid SaveImage(Guid key, byte[] fileData)
        {
            return imageRepository.SaveFile(key, fileData);
        }

        public byte[] LoadImage(Guid key)
        {
            return imageRepository.GetFile(key);
        }

        public void DeleteFile(Guid key)
        {
            imageRepository.DeleteFile(key);
        }
    }
}