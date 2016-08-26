using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.DataModels;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class PostController : BaseController
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        // POST: Posts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel model)
        {


            if (model != null && ModelState.IsValid)
            {
                var post = new Post
                {
                    Body = model.Body,
                    Title = model.Title,
                    Description = model.Description,
                    IsPublic = true,
                    PostedOn = DateTime.Now,
                    UserId = this.User.Identity.GetUserId(),
                };

                db.Posts.Add(post);
                await db.SaveChangesAsync();

                if (model.VideoUrl != null)
                {
                    var postVideo = new PostVideo
                    {
                        PostId = post.PostId,
                        VideoUrl = model.VideoUrl,
                    };
                    db.PostVideos.Add(postVideo);
                    await db.SaveChangesAsync();
                }
                else if (model.ImageUrl != null && ViewBag.Message == 7)
                {
                    var postImage = new PostImage
                    {
                        ImageUrl = model.ImageUrl,
                        PostId = model.Id

                    };
                    db.PostImages.Add(postImage);
                    await db.SaveChangesAsync();
                }

            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return RedirectToAction("Index", "Home");
        }

    }
}