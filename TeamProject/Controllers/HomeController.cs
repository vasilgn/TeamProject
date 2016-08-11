using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            var posts = this.db.Posts.OrderByDescending(p => p.PostedOn)
                    .Select(PostViewModel.ViewModel);

            return this.View(new PostsViewModel()
            {
                Posts = posts
            });
        }
        public ActionResult PostById(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = IsAdmin();
            var postDetails = this.db.Posts
                .Where(p => p.PostId == id)
                .Where(p => p.IsPublic || isAdmin || (p.UserId != null && p.UserId == currentUserId))
                .Select(PostDetailsViewModel.ViewModel).
                FirstOrDefault();

            var isOwner = (postDetails != null && postDetails.AuthorId != null && postDetails.AuthorId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;
            return this.PartialView("_PostDetailsView", postDetails);
        }

        public ActionResult SelectPostOption()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Edit", Value = "0" });

            items.Add(new SelectListItem { Text = "Delete", Value = "1" });


            ViewBag.CanEdit = items;

            return PartialView("_PostOptionsMenu");

        }

        [HttpPost]
        public ActionResult AddLike(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {

                var userName = this.User.Identity.Name;
                var postId = id;
                var postLikesCounter = db.Posts.Find(id).PostLikeCounter;
                var isLike =
                    db.PostLikes.Where(l => l.PostId == postId).Where(l => l.UserName == userName).Select(l => l.Like).FirstOrDefault();
                if (!isLike)
                {
                    var postToUpdate = new Post() { PostId = postId, PostLikeCounter = postLikesCounter + 1 };
                    using (db)
                    {
                        db.Posts.Attach(postToUpdate);
                        db.SaveChangesAsync();
                    }
                    var postLike = new PostLike
                    {
                        Like = true,
                        PostId = postId,
                        UserName = userName
                    };
                    db.PostLikes.Add(postLike);
                    db.SaveChangesAsync();
                }
            }
            return View();


        }
    }

}