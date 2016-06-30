
namespace MovieTutorial.NewsManagement {

    @Serenity.Decorators.registerClass()
    export class TblPrivateNewsGrid extends Serenity.EntityGrid<TblPrivateNewsRow, any> {
        protected getColumnsKey() { return 'NewsManagement.TblPrivateNews'; }
        protected getDialogType() { return TblPrivateNewsDialog; }
        protected getIdProperty() { return TblPrivateNewsRow.idProperty; }
        protected getLocalTextPrefix() { return TblPrivateNewsRow.localTextPrefix; }
        protected getService() { return TblPrivateNewsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}