using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using TeamProject.DataModels;
using TeamProject.Helpers;

namespace TeamProject.Controllers
{
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected BlogDbContext db = new BlogDbContext();

        public string UploadPhoto(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {

                var fileExtension = Path.GetExtension(file.FileName);
                var fnm = Guid.NewGuid() + ".png";


                if (fileExtension.ToLower().EndsWith(".png") || fileExtension.ToLower().EndsWith(".jpg") ||
                    fileExtension.ToLower().EndsWith(".gif"))
                {
                    var filePath = HostingEnvironment.MapPath("~/Content/images/posts/") + fnm;
                    var directory = new DirectoryInfo(HostingEnvironment.MapPath("~/Content/images/posts/"));
                    if (directory.Exists == false)
                    {
                        directory.Create();
                    }
                    try
                    {
                        file.SaveAs(filePath);

                    }
                    catch (Exception e)
                    {

                        Warning("Something went wrong cant upload your picture.", true);
                        throw new Exception(e.Message);
                    }
                    Success("Your picture successfully uploaded to server.", true);

                    var startInx = filePath.LastIndexOf('\\');

                    var lenght = filePath.Length - 1;

                    string shortCut = filePath.Substring(startInx + 1, lenght - startInx);
                    return shortCut;

                }
            }
            return null;

        }

        // Checking user has permissions to get access
        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }

        public string PostTitleById(int postId)
        {
            var currentPost = this.db.Posts.FirstOrDefault(p => p.PostId == postId);
            return currentPost?.Title;
        }
        public void Error(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }
        // pass notifications to TempData dictionary  persist or new list
        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }


        #region HelpMessages
      
        public enum NotifiacationMessage
        {
            AddCommentSuccessfully,
            EditCommentSuccessfully,
            DeleteCommentSuccessfully,
            AddPostSuccessfully,
            EditPostSuccessfully,
            DeletePostSuccessfully,
            AddVideoSuccessfully,
            EditVideoSuccessfully,
            DeleteVideoSuccessfully,
            AddImageSuccessfully,
            EditImageSuccessfully,
            DeleteImageSuccessfully,

            AddLikeSuccessfully,
            AddDislikeSuccessfully,
            AlreadyLikeThis,
            AlreadyDislikeThis,

            Error
        }
        #endregion
    }
}