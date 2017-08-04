using System;

namespace ZLibrary.Model
{

    public class Isbn
    {
        public string Value { get; set; }
        public long Id { get; set; }

        public Isbn()
        {

        }
        public Isbn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            Value = value;
        }

    }

}