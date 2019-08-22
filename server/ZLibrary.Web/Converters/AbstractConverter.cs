namespace ZLibrary.Web.Converters
{
    public abstract class AbstractConverter<TModel, TViewItem> : AbstractFromModelConverter<TModel, TViewItem>, IConverter<TModel, TViewItem>
        where TModel: class
        where TViewItem: class
    {

        public TModel ConvertToModel(TViewItem viewItem)
        {
            return viewItem == null ? null : NullSafeConvertToModel(viewItem);
        }

        protected abstract TModel NullSafeConvertToModel(TViewItem viewItem);
    }
}
