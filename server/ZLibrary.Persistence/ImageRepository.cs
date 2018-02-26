using System;
using System.IO;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public class ImageRepository : IImageRepository
    {
        private readonly object thisLock = new object();

        public Guid SaveFile(Guid key, string filePath)
        {
            lock (thisLock)
            {
                var imageFilePath = GenerateImagePath(key);
                if (File.Exists(filePath))
                {
                    if (File.Exists(imageFilePath))
                    {
                        File.Delete(imageFilePath);
                    }
                    File.Move(filePath, imageFilePath);
                }
            }

            return key;
        }

        public byte[] GetFile(Guid key)
        {
            lock (thisLock)
            {
                byte[] imageData;
                var imageFilePath = GenerateImagePath(key);

                using (var stream = new MemoryStream())
                {
                    if (File.Exists(imageFilePath))
                    {
                        using (var fileStream = File.OpenRead(imageFilePath))
                        {
                            fileStream.CopyTo(stream);
                        }
                    }
                    imageData = stream.ToArray();
                }

                return imageData;
            }
        }

        public void DeleteFile(Guid key)
        {
            lock (thisLock)
            {
                var imageFilePath = GenerateImagePath(key);
                File.Delete(imageFilePath);
            }
        }

        private string GenerateImagePath(Guid key)
        {
            var baseDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(baseDirectory, "BookCoverImages", string.Concat(key.ToString(), ".png"));
        }
    }
}