using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
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
        public async Task<ActionResult> Create(PostViewModel model, HttpPostedFileBase file)
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
                /*if (model.ImageUrl != null)
                {
                    var tryUpload = UploadPhoto(file);

                    if (tryUpload != "Error")
                    {
                        var postImage = new PostImage
                        {
                            ImageUrl = model.ImageUrl,
                            PostId = post.PostId
                        };
                        db.PostImages.Add(postImage);
                        await db.SaveChangesAsync();
                    }
                    
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
            Post post = db.Posts.FirstOrDefault(a=>a.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var toView = new PostCreateModel
            {
                Body = post.Body,
                Description = post.Description,
                ImageUrl = post.PostImages.Where(e => e.PostId == post.PostId).Select(a => a.ImageUrl).FirstOrDefault(),
                Title = post.Title,
                VideoUrl = post.PostVideos.Where(e => e.PostId == post.PostId).Select(a => a.VideoUrl).FirstOrDefault(),
                IsPublic = post.IsPublic
            };
            return View(toView);
        }
        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PostCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var currentPost = db.Posts.FirstOrDefault(p => p.PostId == id);
                var currentVideo = db.PostVideos.FirstOrDefault(p => p.PostId == id);
                var currentImage = db.PostImages.FirstOrDefault(p => p.PostId == id);
                
                if (currentPost != null)
                {

                    currentPost.Body = model.Body;
                    currentPost.Description = model.Description;
                    currentPost.IsPublic = model.IsPublic;
                    currentPost.Title = model.Title;
                    currentPost.Modified = DateTime.Now;

                    db.Entry(currentPost).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    if (model.VideoUrl != null)
                    {
                        currentVideo.VideoUrl = model.VideoUrl;
                        currentVideo.PostId = id;
                        db.Entry(currentVideo).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else if (model.VideoUrl != null && currentVideo == null)
                    {
                        PostVideo postVideo = new PostVideo()
                        {
                            VideoUrl = model.VideoUrl,
                            PostId = id,
                        };
                        db.PostVideos.Add(postVideo);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Entry(currentVideo).State = EntityState.Deleted;
                        await db.SaveChangesAsync();
                    }
                    if (model.ImageUrl != null)
                    {
                        currentImage.ImageUrl = model.ImageUrl;
                        currentImage.PostId = id;
                        db.Entry(currentImage).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else if (model.VideoUrl != null && currentVideo == null)
                    {
                        PostImage postVideo = new PostImage()
                        {
                            ImageUrl = model.VideoUrl,
                            PostId = id,
                        };
                        db.PostImages.Add(postVideo);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Entry(currentImage).State = EntityState.Deleted;
                        await db.SaveChangesAsync();
                    }
                }


                return RedirectToAction("Index","Home");
            }
            return View();
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post currentPost = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (currentPost == null)
            {
                return HttpNotFound();
            }
            return View(currentPost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        /*[HttpPost]
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
                    ViewBag.FilePath = filePath.ToString();
                    file.SaveAs(filePath);
                    return filePath;
                }
            }
            return "Error";

        }*/

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