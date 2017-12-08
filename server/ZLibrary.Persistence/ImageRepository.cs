using System;
using System.IO;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public class ImageRepository : IImageRepository
    {
        public async Task<Guid> SaveFile(Guid key, byte[] imageData)
        {
            var imageFilePath = GenerateImagePath(key);

            using (var stream = new MemoryStream(imageData))
            {
                var dest = File.OpenWrite(imageFilePath);
                await dest.FlushAsync();
                await stream.CopyToAsync(dest);
                dest.Dispose();
            }

            return key;
        }

        public async Task<byte[]> GetFile(Guid key)
        {
            byte[] imageData;
            var imageFilePath = GenerateImagePath(key);

            using (var stream = new MemoryStream())
            {
                if (File.Exists(imageFilePath))
                {
                    await File.OpenRead(imageFilePath).CopyToAsync(stream);
                }
                imageData = stream.ToArray();
            }

            return imageData;
        }

        public void DeleteFile(Guid key)
        {
            var imageFilePath = GenerateImagePath(key);
            File.Delete(imageFilePath);
        }

        private string GenerateImagePath(Guid key)
        {
            var baseDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(baseDirectory, "BookCoverImages", string.Concat(key.ToString(), ".png"));
        }
    }
}