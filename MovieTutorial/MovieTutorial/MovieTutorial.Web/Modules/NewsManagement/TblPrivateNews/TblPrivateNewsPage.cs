

[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "NewsManagement/TblPrivateNews", typeof(MovieTutorial.NewsManagement.Pages.TblPrivateNewsController))]

namespace MovieTutorial.NewsManagement.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("NewsManagement/TblPrivateNews"), Route("{action=index}")]
    public class TblPrivateNewsController : Controller
    {

        public ActionResult CustomizePartialView()
        {
            return PartialView();
        }

        //[PageAuthorize("Administration")]
        public ActionResult Index()
        {
            //return PartialView();
            return View("~/Modules/NewsManagement/TblPrivateNews/TblPrivateNewsIndex.cshtml");
        }
    }
}