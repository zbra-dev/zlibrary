namespace ZLibrary.Web.Converters
{
    public interface IFromModelConverter<TModel, TViewItem>
    {
        TViewItem ConvertFromModel(TModel model);
    }
}
