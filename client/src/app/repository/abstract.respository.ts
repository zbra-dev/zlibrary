export class AbstractRepository {
    public Copy<TViewItem, TModel>(viewItem: TViewItem, model: TModel): TModel {
        return Object.assign(viewItem, model);
    }
}
