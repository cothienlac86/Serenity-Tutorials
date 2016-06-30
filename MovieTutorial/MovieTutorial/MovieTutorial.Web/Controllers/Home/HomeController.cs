using MovieTutorial.Entity;
using MovieTutorial.Generate;
using MovieTutorial.Models;
using System.Web.Mvc;

namespace MovieTutorial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["UIConvert"] == null)
            {
                var area = new AreaDb();
                ViewBag.areaList = area.GetAll();
                // Lay danh sach chuyen muc
                var menu = new MenuDb();
                var listAll = menu.GetAll();
                var listAfterConvert = BdsCommon.GetTree(listAll, 0);
                BdsCommon.GetMenuUlTag(listAfterConvert);
                BdsCommon.GetMenuSelectTag(listAfterConvert);
                //Common.SaveDataToResource("NavCategory", Common.ListMenuSelectTag.ToString());
                ViewBag.categoryList = BdsCommon.ListMenuSelectTag.ToString();
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ViewResult CreateMenu()
        {
            //Get Parent Menu
            var menu = new MenuDb();
            ViewBag.ListParentMenu = menu.GetParentMenu();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMenu(MenuModels menuModels)
        {
            var menuDb = new MenuDb();
            menuModels = menuDb.Add(menuModels);
            RefreshMenu();
            Session["AddMenuSuccessMessage"] = "Thêm menu thành công!";
            return RedirectToAction("EditMenu", "Home", new { @Id = menuModels.Id });
        }

        [Authorize]
        [HttpGet]
        public ViewResult EditMenu(int Id = 1)
        {
            //Get Parent Menu
            var menuDb = new MenuDb();
            ViewBag.ListParentMenu = menuDb.GetParentMenu();
            //GetMenuById
            var model = menuDb.GetMenuById(Id);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenu(MenuModels menuModels)
        {
            var menuDb = new MenuDb();
            menuDb.Update(menuModels);
            ViewBag.ListParentMenu = menuDb.GetParentMenu();
            RefreshMenu();
            Session["AddMenuSuccessMessage"] = "Sửa menu thành công!";
            return View(menuModels);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Menu()
        {
            RefreshMenu();
            ViewBag.listMenuBasic = BdsCommon.ListMenuBasic;
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult DeleteMenu(int Id)
        {
            var id = Id;
            var menuDb = new MenuDb();
            menuDb.DeleteMenuById(Id);
            RefreshMenu();
            return Json("Delete complete", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Refresh menu
        /// </summary>
        private void RefreshMenu()
        {
            var menuDb = new MenuDb();
            //Update menu again
            BdsCommon.ListMenuUlTag.Clear();
            BdsCommon.ListMenuSelectTag.Clear();
            BdsCommon.ListMenuBasic.Clear();
            var list = menuDb.GetAll();
            var listAfterConvert = BdsCommon.GetTree(list, 0);
            BdsCommon.GetMenuUlTag(listAfterConvert);
            BdsCommon.GetMenuSelectTag(listAfterConvert);
            BdsCommon.GetMenuList(listAfterConvert);
            System.Web.HttpContext.Current.Session["UIConvert"] = BdsCommon.ListMenuUlTag.ToString();
            System.Web.HttpContext.Current.Session["UIMenu"] = BdsCommon.ListMenuSelectTag.ToString();
        }
    }
}
