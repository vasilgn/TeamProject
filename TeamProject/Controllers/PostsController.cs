using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.DataModels;
using TeamProject.Models;


namespace TeamProject.Controllers
{
    public class PostsController : BaseController
    {
        public ActionResult PostById(int Id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = IsAdmin();
            var postDetails = this.db.Posts
                .Where(p => p.PostId == Id)
                .Where(p => p.IsPublic || isAdmin || (p.UserId != null && p.UserId == currentUserId))
                .Select(PostDetailsViewModel.ViewModel).
                FirstOrDefault();

            var isOwner = (postDetails != null && postDetails.AuthorId != null && postDetails.AuthorId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;
            return this.PartialView("_PostDetails", postDetails);
        }

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
        public async Task<ActionResult> Create(PostViewModel model)
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
