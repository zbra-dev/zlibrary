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

        public async Task<Guid> SaveImage(byte[] fileData, string contentType)
        {
            return await imageRepository.SaveFile(fileData);
        }
    }
}