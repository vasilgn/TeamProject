using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace TeamProject.Extensions
{
    public class HtmlHelperJson
    {
        private static readonly JsonSerializerSettings settings;

        static HtmlHelperJson()
        {
            settings = new JsonSerializerSettings();
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, object value)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(value, Formatting.None, settings));
        }
    }
}