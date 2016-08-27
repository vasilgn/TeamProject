using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    [Authorize]
    public class PostController : BaseController
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Posts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostCreateModel model)
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
                /* else if (model.ImageUrl != null && ViewBag.Message == 7)
                 {
                     var postImage = new PostImage
                     {
                         ImageUrl = model.ImageUrl,
                         PostId = model

                     };
                     db.PostImages.Add(postImage);
                     await db.SaveChangesAsync();
                 }*/

            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post =  db.Posts.FirstOrDefault(x => x.PostId == id);
            var toView = new PostCreateModel
            {
                Body = post.Body,
                Description = post.Description,
                ImageUrl = post.PostImages.Where(e => e.PostId == post.PostId).Select(a => a.ImageUrl).FirstOrDefault(),
                Title = post.Title,
                VideoUrl = post.PostVideo.Where(e => e.PostId == post.PostId).Select(a => a.VideoUrl).FirstOrDefault(),
                IsPublic = post.IsPublic

            };
            return View(toView);
        }
        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostCreateModel post)
        {

            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}