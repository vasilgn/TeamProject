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
        public ActionResult LikeDislikeIncrement(int? id, string submit, bool? Like)
        {

            var post = db.Posts.Find(id);

            switch (submit)
            {
                case "Like":
                    post.PostLikeCounter++;

                    break;
                case "Dislike":
                    post.PostLikeCounter--;
                    break;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}