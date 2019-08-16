using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZLibrary.Web.Converters
{
    public interface IToModelConverter<TModel, TViewItem>
    {
        TModel ConvertToModel(TViewItem dto);
    }
}
