using SportsStore2.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.HtmlHelpers {
    public static class HtmlExtensions {
        public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> link) {
            StringBuilder sb = new StringBuilder();
            TagBuilder tag = new TagBuilder("a");
            for (int i = 1; i <= pagingInfo.NrOfPages; i++) {
                if (pagingInfo.CurrentPage == i) {
                    tag.AddCssClass("selected");
                }
                // kad će se pozivati ova metoda:
                // PageLinks(pagingInfo, x => new UrlHelper().Action("List", "Product", new { page = x })
                tag.MergeAttribute("href", link(i));
                // Linija iznad je ista stvar kao da si i ovo ispod pozvao direktno.
                //tag.MergeAttribute("href", new UrlHelper().Action("List", "Product", new { page = i }));
                tag.InnerHtml = i.ToString();
                sb.Append(tag);
            }

            return null;
        }
    }
}