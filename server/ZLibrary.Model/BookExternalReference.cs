using System;

namespace ZLibrary.Model
{

    public class BookExternalReference
    {

        public string Value { get; set; }

        public BookExternalReference(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            Value = value;
        }

    }

}