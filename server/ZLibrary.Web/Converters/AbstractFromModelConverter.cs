namespace ZLibrary.Web.Converters
{
    public abstract class AbstractFromModelConverter<TModel, TViewItem> : IFromModelConverter<TModel, TViewItem>
        where TModel: class
        where TViewItem: class
    {
        public TViewItem ConvertFromModel(TModel model)
        {
            return model == null ? null : NullSafeConvertFromModel(model);
        }

        protected abstract TViewItem NullSafeConvertFromModel(TModel model);
    }
}
