using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.DataModels;
using TeamProject.Helpers;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        // GET: Post
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts);
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
                Success("Successfully create post.", true);
                if (model.VideoUrl != null)
                {
                    string s = YouTubeUrlHandler.GetVideoId(model.VideoUrl);
                    if (s != "Error")
                    {

                        var postVideo = new PostVideo
                        {
                            PostId = post.PostId,
                            VideoUrl = s,
                        };
                        db.PostVideos.Add(postVideo);
                        await db.SaveChangesAsync();
                        Success("Video to post was successfully added.", true);
                    }

                }
                if (file?.FileName != null)
                {
                    var tryUpload = UploadPhoto(file);

                    if (tryUpload != "Error")
                    {
                        var startInx = tryUpload.LastIndexOf('\\');

                        var lenght = tryUpload.Length-1;

                        string shortCut = tryUpload.Substring(startInx+1, lenght - startInx);
                        var postImage = new PostImage
                        {
                            ImageUrl = shortCut,
                            PostId = post.PostId
                        };
                        db.PostImages.Add(postImage);
                        await db.SaveChangesAsync();
                    }

                }
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
            Post post = db.Posts.FirstOrDefault(a => a.PostId == id);
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
                    Information("You have successfully edit post.", true);


                    if (model.VideoUrl != null && currentVideo != null)
                    {
                        currentVideo.VideoUrl = YouTubeUrlHandler.GetVideoId(model.VideoUrl);
                        currentVideo.PostId = id;
                        db.Entry(currentVideo).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        Information("You have successfully edit video url.", true);
                    }
                    else if (model.VideoUrl != null && currentVideo == null)
                    {
                        PostVideo postVideo = new PostVideo()
                        {
                            VideoUrl = YouTubeUrlHandler.GetVideoId(model.VideoUrl),
                            PostId = id,
                        };
                        db.PostVideos.Add(postVideo);
                        await db.SaveChangesAsync();
                        Success("Successfully add video to post.");
                    }
                    else
                    {
                        db.Entry(currentVideo).State = EntityState.Deleted;
                        await db.SaveChangesAsync();
                        Information("Video to this post was deleted.", true);


                    }
                    //Add Edit Image
                    if (model.ImageUrl != null && currentImage != null)
                    {
                        currentImage.ImageUrl = model.ImageUrl;
                        currentImage.PostId = id;
                        db.Entry(currentImage).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else if (model.ImageUrl != null && currentImage == null)
                    {
                        PostImage postImage = new PostImage()
                        {
                            ImageUrl = model.VideoUrl,
                            PostId = id,
                        };
                        db.PostImages.Add(postImage);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Entry(currentImage).State = EntityState.Deleted;
                        await db.SaveChangesAsync();
                    }
                }


                return RedirectToAction("Index", "Home");
            }
            Error("Something went wrong.", true);
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

            var json = new
            {
                postId = currentPost.PostId,

            };

            return Json(json, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteComfirmed(string id)
        {
            try
            {
                var postId = int.Parse(id);
                Post post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
                Information("You have successfully delete this post.", true);
                return Json("Success", "Home");
            }
            catch (Exception e)
            {
                return Json("Error",e.ToString());

            }
            

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