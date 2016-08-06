using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.DataModels;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class PostController : BaseController
    {

        // GET: Post
        public ActionResult Index()
        {

            var posts = db.Posts
                .OrderBy(p => p.PostedOn)

                .Select(PostViewModel.ViewModel);

            return View();
        }
        [HttpPost]
        public ActionResult LikeDislikeIncrement(int? id, string submit )
        {

            var post = db.Posts.Find(id);

            if (Convert.ToBoolean(Request["isLike"]) == true)
            {
                post.PostLikeCounter++;
            }
            else { post.PostLikeCounter--; }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
		}
}