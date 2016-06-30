
namespace MovieTutorial.NewsManagement {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class TblPrivateNewsDialog extends Serenity.EntityDialog<TblPrivateNewsRow, any> {
        protected getFormKey() { return TblPrivateNewsForm.formKey; }
        protected getIdProperty() { return TblPrivateNewsRow.idProperty; }
        protected getLocalTextPrefix() { return TblPrivateNewsRow.localTextPrefix; }
        protected getNameProperty() { return TblPrivateNewsRow.nameProperty; }
        protected getService() { return TblPrivateNewsService.baseUrl; }

        protected form = new TblPrivateNewsForm(this.idPrefix);
    }
}