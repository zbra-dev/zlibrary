using System;

namespace ZLibrary.Model
{
    public class BookExternalReference
    {
        public static readonly BookExternalReference Empty = new BookExternalReference();

        private string _value;

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }
                _value = value;
            }
        }

        public BookExternalReference()
        {
            _value = string.Empty;
        }
    }
}