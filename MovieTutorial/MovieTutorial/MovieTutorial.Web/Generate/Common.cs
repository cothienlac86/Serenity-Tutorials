using MovieTutorial.Entity;
using MovieTutorial.Models;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace MovieTutorial.Generate
{
    /// <summary>
    /// Common function
    /// </summary>
    public static class BdsCommon
    {
        #region Public Fields

        public static StringBuilder CategoryBuilder = new StringBuilder();
        public static List<MenuModels> ListMenuBasic = new List<MenuModels>();
        public static StringBuilder ListMenuSelectTag = new StringBuilder();
        public static StringBuilder ListMenuUlTag = new StringBuilder();

        #endregion Public Fields

        #region Public Methods

        /// <summary>
        /// Get list link for edit menu
        /// </summary>
        /// <param name="list"></param>
        public static void GetMenuList(List<Tree> list)
        {
            foreach (var item in list)
            {
                ListMenuBasic.Add(new MenuModels
                {
                    Name = item.Name,
                    Id = item.Id
                });
                if (item.List.Count > 0)
                {
                    GetMenuList(item.List);
                }
            }
        }

        /// <summary>
        /// Get menu with option for select tag
        /// </summary>
        /// <param name="list"></param>
        /// <param name="source"></param>
        public static void GetMenuSelectTag(List<Tree> list)
        {
            if (!string.IsNullOrEmpty(ListMenuSelectTag.ToString())) return;
            foreach (var item in list)
            {
                if (item.Id > 48) continue;
                if (item.List.Count > 0)
                {
                    ListMenuSelectTag.AppendLine("<optgroup label='" + item.Name + "'>");
                    GetMenuSelectTag(item.List);
                    ListMenuSelectTag.AppendLine("</optgroup>");
                }
                else
                {
                    ListMenuSelectTag.AppendLine("<option value='" + item.Id + "'>" + item.Name + "</option>");
                }
            }
        }

        /// <summary>
        /// Get menu with ul,li for ul tag
        /// </summary>
        /// <param name="list"></param>
        /// <param name="source"></param>
        public static void GetMenuUlTag(List<Tree> list, int source = 0)
        {
            if (source == 0)
                ListMenuUlTag.AppendLine("<ul class='nav navbar-nav'>");
            else
                ListMenuUlTag.AppendLine("<ul class='dropdown-menu'>");
            var loop = 1;
            foreach (var item in list)
            {
                if (item.List.Count > 0)
                {
                    ListMenuUlTag.AppendLine("<li id='" + item.Id + "' class='dropdown' >");
                    ListMenuUlTag.AppendLine("<a href'#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>" + item.Name + "<span class='caret'></span></a>");
                }
                else
                {
                    ListMenuUlTag.AppendLine("<li id='" + item.Id + "' >");
                    ListMenuUlTag.AppendLine("<a href='/News/ShowPost/" + item.Id + "'>" + item.Name + "</a>");
                    if (loop < list.Count)
                        ListMenuUlTag.AppendLine("<li role='separator' class='divider'></li>");
                }

                if (item.List.Count > 0)
                {
                    GetMenuUlTag(item.List, 1);
                }
                ListMenuUlTag.AppendLine("</li>");
                loop++;
            }
            ListMenuUlTag.AppendLine("</ul>");
        }

        /// <summary>
        /// Get menu with option for select tag
        /// </summary>
        /// <param name="list"></param>
        /// <param name="source"></param>
        public static void GetParentCategory(List<Tree> list, int selectedId = 0)
        {
            foreach (var item in list)
            {
                if (item.Id > 48) continue;
                if (item.List.Count > 0)
                {
                    CategoryBuilder.AppendLine("<optgroup label='" + item.Name + "'>");
                    GetParentCategory(item.List);
                    CategoryBuilder.AppendLine("</optgroup>");
                }
                else
                {
                    if (selectedId == item.Id)
                        CategoryBuilder.AppendLine("<option value='" + item.Id + "' selected='true'>" + item.Name + "</option>");
                    else
                        CategoryBuilder.AppendLine("<option value='" + item.Id + "'>" + item.Name + "</option>");
                }
            }
        }

        /// <summary>
        /// Get list tree
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Tree> GetTree(List<MenuDb> list, int parent)
        {
            return list.Where(x => x.ParentId == parent).Select(x => new Tree
            {
                Id = x.Id,
                Name = x.Name,
                List = GetTree(list, x.Id)
            }).ToList();
        }
        public static string LoadResourceData(string key)
        {
            //string apPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Properties/Resources.resx");
            var nav = string.Empty;
            var rd = new ResourceReader(
                System.Web.Hosting.HostingEnvironment.MapPath(@"~\Properties\Resources.resource"));
            if (rd == null) return string.Empty;
            string resType = string.Empty;
            byte[] valOut;
             rd.GetResourceData("NavCategory", out resType, out valOut);
            if (valOut != null)
            {
                nav = System.Text.Encoding.UTF8.GetString(valOut);
            }
            return nav;
        }

        public static void SaveDataToResource(string key, string val)
        {
            try
            {

                var rw = new ResourceWriter(System.Web.Hosting.HostingEnvironment.MapPath(@"~\Properties\Resources.resource"));
                if (rw == null) return;
                rw.AddResource(key, val);
                rw.Generate();
                rw.Close();
                rw.Dispose();
            }
            catch
            {
                throw;
            }
        }

        #endregion Public Methods
    }
}
