using System.Collections.Generic;

namespace ZLibrary.API
{
    public class BookSearchParameter
    {
        public string Keyword {get; private set;}

        public SearchOrderBy OrderBy { get; set; }

        public BookSearchParameter(string keyword)
        {
            Keyword = keyword;
        }
    }

    public enum SearchOrderBy 
    {
        Title = 0,
        Created = 1
    }
}