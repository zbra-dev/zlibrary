using System;

namespace ZLibrary.Model
{
    public class Isbn
    {
        private readonly string isbnCode;

        public static Isbn FromString(string isbnCode)
        {
            if(isbnCode == null)
            {
                throw new ArgumentNullException("Isbn code is not defined");
            }
            return new Isbn(isbnCode);
        }

        private Isbn(string isbnCode)
        {
            this.isbnCode = isbnCode;
        }

        public override string ToString()
        {
            return isbnCode;
        }

        public override int GetHashCode()
        {
            return isbnCode.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is Isbn other) && other.isbnCode.Equals(isbnCode); 
        }

    }
}
