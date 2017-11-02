using System;

namespace ZLibrary.Model
{
    public class CoverImage
    {
        public static readonly CoverImage Empty = new CoverImage();
        
        public long Id { get; set; }
        public byte[] Image { get; set; }

        public CoverImage()
        : this(new byte[] {})
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