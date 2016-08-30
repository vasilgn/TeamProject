using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace TeamProject.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");
            return new MvcHtmlString(string.Format(@"{0}:""{1}""", "__RequestVerificationToken", tokenValue));
        }

        public static UrlHelper UrlHelper(this HtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewContext.Controller is Controller)
                return ((Controller)htmlHelper.ViewContext.Controller).Url;

            const string itemKey = "HtmlHelper_UrlHelper";

            if (htmlHelper.ViewContext.HttpContext.Items[itemKey] == null)
                htmlHelper.ViewContext.HttpContext.Items[itemKey] = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

            return (UrlHelper)htmlHelper.ViewContext.HttpContext.Items[itemKey];
        }

        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string rawHtml, string action, string controller, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            //string anchor = ajaxHelper.ActionLink("##holder##", action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();
            //return MvcHtmlString.Create(anchor.Replace("##holder##", rawHtml));
            /* Updated code to use a generated guid as from */
            string holder = Guid.NewGuid().ToString();
            string anchor = ajaxHelper.ActionLink(holder, action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }

        public static MvcHtmlString RawActionLink(this HtmlHelper htmlHelper, string rawHtml, string action, string controller, object routeValues, object htmlAttributes)
        {
            //string anchor = htmlHelper.ActionLink("##holder##", action, controller, routeValues, htmlAttributes).ToString();
            //return MvcHtmlString.Create(anchor.Replace("##holder##", rawHtml));
            /* Updated code to use a generated guid as from  */
            string holder = Guid.NewGuid().ToString();
            string anchor = htmlHelper.ActionLink(holder, action, controller, routeValues, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }

    }
}