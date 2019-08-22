namespace ZLibrary.Web.Converters
{
    public interface IToModelConverter<TModel, TViewItem>
    {
        TModel ConvertToModel(TViewItem viewItem);
    }
}
