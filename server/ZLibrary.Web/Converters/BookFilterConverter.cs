using System;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class BookFilterConverter : AbstractToModelConverter<BookFilter, SearchParametersDto>
    {
        protected override BookFilter NullSafeConvertToModel(SearchParametersDto viewItem)
        {
            return new BookFilter
            {
                FreeSearch = viewItem.Keyword,
                OrderBy = (SearchOrderBy)Enum.ToObject(typeof(SearchOrderBy), viewItem.OrderByValue)
            };
        }
    }
}
