using System;

namespace ZLibrary.Model
{
    public class BookExternalReference
    {
        // public static readonly BookExternalReference Empty = new BookExternalReference(string.Empty);

        public string Value { get; set; }
        public BookExternalReference(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"The paramenter {nameof(value)} can not be null or empty.");
            }
            Value = value;
        }
    }
}