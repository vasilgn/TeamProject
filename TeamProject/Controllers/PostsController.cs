using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.DataModels;
using TeamProject.Models;


namespace TeamProject.Controllers
{
    public class PostsController : BaseController
    {
        

        // GET: Posts
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(await posts.ToListAsync());
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

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Posts/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel model,HttpPostedFileBase file)
        {

            if (model != null && ModelState.IsValid)
            {
                

                var post = new Post
                {
                    Body = model.Body,
                    Title = model.Title,
                    PostedOn = DateTime.Now,
                    UserId = this.User.Identity.GetUserId(),


                };
                db.Posts.Add(post);
                await db.SaveChangesAsync();

                if (file != null && file.ContentLength > 0)
                {

                    var fileExtension = Path.GetExtension(file.FileName);
                    var fnm = Guid.NewGuid() + ".png";


                    if (fileExtension.ToLower().EndsWith(".png") || fileExtension.ToLower().EndsWith(".jpg") || fileExtension.ToLower().EndsWith(".gif"))
                    {
                        var filePath = HostingEnvironment.MapPath("~/Content/images/posts/") + fnm;
                        var directory = new DirectoryInfo(HostingEnvironment.MapPath("~/Content/images/posts/"));
                        if (directory.Exists == false)
                        {
                            directory.Create();
                        }
                        ViewBag.FilePath = filePath.ToString();
                        file.SaveAs(filePath);
                        var postImage = new PostImage
                        {
                            ImageUrl = filePath.ToString(),
                            PostId = db.Posts.Last().PostId
                        };
                        db.PostImages.Add(postImage);
                        await db.SaveChangesAsync();
                    }

                }
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // GET: Posts/Edit/5
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
        public async Task<ActionResult> Edit([Bind(Include = "PostId,Title,Body,Description,PostedOn,Modified,PostLikeCounter,UserId,IsPublic")] Post post)
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
            return RedirectToAction("Index");
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
