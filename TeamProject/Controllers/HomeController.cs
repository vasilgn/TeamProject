using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;
using System.Data.Entity;
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
    }

}