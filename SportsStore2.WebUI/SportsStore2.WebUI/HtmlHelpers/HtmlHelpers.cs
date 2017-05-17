using SportsStore2.WebUI.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace SportsStore2.WebUI.HtmlHelpers {
    public static class HtmlHelpers {
        public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> link) {
            StringBuilder sb = new StringBuilder();
            TagBuilder tag;
            for (int i = 1; i <= pagingInfo.NrOfPages; i++) {
                tag = new TagBuilder("a");
                if (pagingInfo.CurrentPage == i) {
                    tag.AddCssClass("selected");
                }
                tag.MergeAttribute("href", link(i));
                tag.InnerHtml = i.ToString();
                sb.Append(tag);
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}