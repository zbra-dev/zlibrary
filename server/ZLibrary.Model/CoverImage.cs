namespace ZLibrary.Model
{

    public class CoverImage
    {
        public byte[] Image { get; set; }
        public long Id { get; set; }

        public CoverImage()
        {

        }

        public CoverImage(byte[] image)
        {
            Image = image;
        }

    }

}