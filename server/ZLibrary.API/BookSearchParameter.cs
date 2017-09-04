using System.Collections.Generic;

namespace ZLibrary.API
{
    public class BookSearchParameter
    {
        public string Keyword {get; private set;}

        public BookSearchParameter(string keyword)
        {
            Keyword = keyword;
        }
    }
}