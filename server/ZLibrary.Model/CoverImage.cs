using System;

namespace ZLibrary.Model
{
    public class CoverImage
    {
        public static readonly CoverImage Empty = new CoverImage(new byte[] {});
        public byte[] Image { get; set; }
        public long Id { get; set; }

        public CoverImage()
        {
        }
        public CoverImage(byte[] image)
        {
            if (image == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(image)} can not be null.");
            }
            Image = image;
        }
    }
}