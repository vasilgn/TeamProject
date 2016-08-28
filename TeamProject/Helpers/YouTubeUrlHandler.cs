namespace TeamProject.Helpers
{
    public class YouTubeUrlHandler
    {
        

        const string EmbedUrl = @"http://www.youtube.com/embed/";
        public static string GetVideoId(string url)
        {
            

                int pos1 = url.IndexOf("?v=");
                int pos2 = url.IndexOf("&");
                string videoCode = pos2 > pos1 ? url.Substring(pos1 + 3, pos2 - pos1 - 3) : url.Substring(pos1 + 3);
                return EmbedUrl + videoCode;
        }
    }
}