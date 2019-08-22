export class AbstractRepository {
    public copy<TViewItem, TModel>(viewItem: TViewItem, model: TModel): TModel {
        return Object.assign(viewItem, model);
    }
}
