using System;

namespace ZLibrary.API
{
    public class ImageSaveException : Exception
    {
        public ImageSaveException(string message) : base(message)
        {
        }
    }
}