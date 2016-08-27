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

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,Title,Body,Description,UserId,IsPublic")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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