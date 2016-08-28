using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TeamProject.Handlers
{
    public class YouTubeUrlHandler
    {
        const string Patern =
            "^(?:https?\\:\\/\\/)?(?:www\\.)?(?:youtu\\.be\\/|youtube\\.com\\/(?:embed\\/|v\\/|watch\\?v\\=))([\\w-]{10,12})(?:$|\\&|\\?\\#).*";

        const string EmbedUrl = @"http://www.youtube.com/embed/";
        public static string GetVideoId(string url)
        {
            Regex rg = new Regex(Patern);
            Match match = rg.Match(url);

            if (match.Success)
            {
                int pos1 = url.IndexOf("?v=");
                int pos2 = url.IndexOf("&");
                string videoCode = pos2 > pos1 ? url.Substring(pos1 + 3, pos2 - pos1 - 3) : url.Substring(pos1 + 3);
                string m = EmbedUrl + videoCode;
                return EmbedUrl + videoCode;
            }
            return "Error";
        }
    }
}