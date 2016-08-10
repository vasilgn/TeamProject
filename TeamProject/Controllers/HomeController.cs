using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;
using System.Data.Entity;
using System.Web.Mvc;
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
    }
}