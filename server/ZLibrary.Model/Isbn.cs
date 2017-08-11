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
        public Isbn(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException($"The paramenter {nameof(isbn)} can not be null or empty.");
            }
            Value = isbn;
        }
    }
}