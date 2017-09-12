using System;
using System.IO;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public class ImageRepository : IImageRepository
    {
        public async Task<Guid> SaveFile(byte[] imageData)
        {
            var imageId = Guid.NewGuid();
            var baseDirectory = Directory.GetCurrentDirectory();
            var imageFilePath = Path.Combine(baseDirectory, imageId.ToString(), ".png");

            using (var stream = new MemoryStream(imageData))
            {
                await File.OpenWrite(imageFilePath).CopyToAsync(stream);
            }

            return imageId;
        }
    }
}