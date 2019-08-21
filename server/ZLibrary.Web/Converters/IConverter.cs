namespace ZLibrary.Web.Converters
{
    public interface IConverter<TModel, TViewItem> : IToModelConverter<TModel, TViewItem>, IFromModelConverter<TModel, TViewItem>
    {
    }
}
