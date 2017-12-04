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

        public async Task<Guid> SaveImage(Guid key, byte[] fileData)
        {
            return await imageRepository.SaveFile(key, fileData);
        }

        public async Task<byte[]> LoadImage(Guid key)
        {
            return await imageRepository.GetFile(key);
        }

        public void DeleteFile(Guid key){
            imageRepository.DeleteFile(key);
        }

    }
}