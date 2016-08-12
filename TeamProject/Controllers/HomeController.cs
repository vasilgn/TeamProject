using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.Extensions;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
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

       /* [HttpGet]
        public ActionResult AddLike(int? id)
        {

            return RedirectToAction("AddLike",id);
        }*/

        [HttpPost]
        public ActionResult AddLike(PostsViewModel model,int id)
        {
            
            if (ModelState.IsValid && model != null)
            {

                var userName = this.User.Identity.Name;
                var postId = id;
                var isLike =
                    db.PostLikes.Where(l => l.PostId == postId).Where(l => l.UserName == userName).Select(l => l.Like).FirstOrDefault();
                if (!isLike)
                {
                    Post post;
                    using (var ctx = new BlogDbContextEntities())
                    {
                        post = ctx.Posts.FirstOrDefault(s => s.PostId == postId);
                    }
                    if (post != null)
                    {
                        post.PostLikeCounter += 1;
                    }
                    using (var dbCtx = new BlogDbContextEntities())
                    {
                        //3. Mark entity as modified
                        dbCtx.Entry(post).State = System.Data.Entity.EntityState.Modified;

                        //4. call SaveChanges
                        dbCtx.SaveChanges();
                    }

                    
                    var postLike = new PostLike
                    {
                        Like = true,
                        PostId = postId,
                        UserName = userName
                    };
                    db.PostLikes.Add(postLike);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }

}