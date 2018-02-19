using System;
using System.IO;
using System.Threading.Tasks;

namespace ZLibrary.Persistence
{
    public class ImageRepository : IImageRepository
    {
        private readonly object thisLock = new object();

        public Guid SaveFile(Guid key, byte[] imageData)
        {
            lock (thisLock)
            {
                var imageFilePath = GenerateImagePath(key);

                using (var stream = new MemoryStream(imageData))
                using (var dest = File.OpenWrite(imageFilePath))
                {
                    dest.Flush();
                    stream.CopyTo(dest);
                }

                return key;
            }
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