using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class BookFilter
    {
        public string FreeSearch {get;  set;}
        public SearchOrderBy OrderBy { get; set; }
        public bool AllowNoCopies { get; set; }
    }

    public enum SearchOrderBy 
    {
        Title = 0,
        Created = 1
    }
}