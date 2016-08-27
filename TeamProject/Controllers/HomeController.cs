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
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index(DbChangeMessageId? message)
        {
            ViewBag.StatusMessage =
                message == DbChangeMessageId.AddCommentSuccessfully ? "Add comment successfully."
                    : message == DbChangeMessageId.DislikeSuccessfully ? "Disliked."
                    : message == DbChangeMessageId.LikeSuccessfully ? "Liked."
                    : message == DbChangeMessageId.Error ? "Error."
                    : "";
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

        //
        //POST Add comment
        [HttpPost]
        public async Task<ActionResult> AddComment(CommentViewModel model, int id, string commentText)
        {
            if (model != null && ModelState.IsValid)
            {

                var userId = this.User.Identity.GetUserId();
                var newComment = new Comment()
                {
                    PostId = id,
                    CommentDate = DateTime.Now,
                    Text = commentText,
                    UserId = userId,
                    CommentLikeCounter = 0,

                };
                this.db.Comments.Add(newComment);
                await this.db.SaveChangesAsync();
                ModelState.Clear();
                var thisComments = db.Comments.Where(c=>c.PostId == id).AsEnumerable().LastOrDefault();
                
                return Json(new { model = thisComments });
            }
            return View("Index");
        }

        //
        //POST Like

        [HttpPost]
        public ActionResult AddLike(PostViewModel model, int id, string command)
        {
            DbChangeMessageId? message;

            if (ModelState.IsValid && model != null)
            {

                var userName = this.User.Identity.Name;
                var postId = id;

                var isLike =
                    db.PostLikes.Where(l => l.PostId == postId).Where(l => l.UserName == userName).Select(l => l.Like).FirstOrDefault();
                var postLikeId =
                    db.PostLikes.Where(l => l.PostId == postId).Where(l => l.UserName == userName).Select(l => l.PostLikeId).FirstOrDefault();

                Post post = db.Posts.FirstOrDefault(s => s.PostId == postId);
                PostLike postLike = db.PostLikes.FirstOrDefault(l => l.PostLikeId == postLikeId);

                if (post != null)
                {
                    if (postLike != null)
                    {
                        if (isLike && command.Equals("Dislike"))
                        {
                            post.PostLikeCounter -= 2;
                            postLike.Like = false;
                        }
                        else if (!isLike && command.Equals("Like"))
                        {
                            post.PostLikeCounter += 2;
                            postLike.Like = true;
                        }

                        db.Entry(postLike).State = EntityState.Modified;
                        db.SaveChanges();
                        message = DbChangeMessageId.LikeSuccessfully;
                    }
                    else
                    {
                        var newPostLike = new PostLike()
                        {
                            UserName = userName,
                            PostId = postId,

                        };
                        if (command.Equals("Like"))
                        {
                            post.PostLikeCounter += 1;
                            newPostLike.Like = true;
                        }
                        else if (command.Equals("Dislike"))
                        {
                            post.PostLikeCounter -= 1;
                            newPostLike.Like = false;
                        }
                        db.PostLikes.Add(newPostLike);
                        db.SaveChanges();
                    }
                }

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();


                message = DbChangeMessageId.LikeSuccessfully;


                var postLikeCount = db.Posts.Find(id).PostLikeCounter;
                var postDislikes = db.PostLikes.Where(l => l.PostId == postId).Count(l => l.Like == false);
                var postLikes = db.PostLikes.Where(l => l.PostId == postId).Count(l => l.Like);
                return Json(new
                {
                    postId = postId,
                    postDislikeCount = postDislikes,
                    postLikeCount = postLikes,
                    postLikes = postLikeCount,
                    statusMessage = message
                });
                //return RedirectToAction("Index");
            }
            message = DbChangeMessageId.Error;
            return RedirectToAction("Index", new { Message = message });
        }


        //
        //POST Comment Like
        [HttpPost]
        public ActionResult CommentLike(CommentViewModel model, int id, string command)
        {
            DbChangeMessageId message;

            if (ModelState.IsValid && model != null)
            {

                var userName = this.User.Identity.Name;
                var commentId = id;

                var isLike =
                    db.CommentsLikes.Where(l => l.CommentId == commentId).Where(l => l.UserName == userName).Select(l => l.Like).FirstOrDefault();
                var commentLikeId =
                    db.CommentsLikes.Where(l => l.CommentId == commentId)
                        .Where(l => l.UserName == userName)
                        .Select(l => l.CommentLikeId)
                        .FirstOrDefault();


                Comment comment = db.Comments.FirstOrDefault(s => s.CommentId == commentId);
                CommentLike commentLike = db.CommentsLikes.FirstOrDefault(l => l.CommentLikeId == commentLikeId);

                if (comment != null)
                {
                    if (commentLike != null)
                    {
                        if (isLike && command.Equals("Dislike"))
                        {
                            comment.CommentLikeCounter -= 2;
                            commentLike.Like = false;
                            message = DbChangeMessageId.DislikeSuccessfully;

                        }
                        else if (!isLike && command.Equals("Like"))
                        {
                            comment.CommentLikeCounter += 2;
                            commentLike.Like = true;
                            message = DbChangeMessageId.LikeSuccessfully;

                        }

                        db.Entry(commentLike).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var newCommentLike = new CommentLike()
                        {
                            UserName = userName,
                            CommentId = commentId,

                        };
                        if (command.Equals("Like"))
                        {
                            comment.CommentLikeCounter += 1;
                            newCommentLike.Like = true;
                            message = DbChangeMessageId.LikeSuccessfully;

                        }
                        else if (command.Equals("Dislike"))
                        {
                            comment.CommentLikeCounter -= 1;
                            newCommentLike.Like = false;
                            message = DbChangeMessageId.DislikeSuccessfully;

                        }
                        db.CommentsLikes.Add(newCommentLike);
                        db.SaveChanges();
                    }
                }

                db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var commentLikeCount = db.Comments.Find(commentId).CommentLikeCounter;
                var commenDislikes = db.CommentsLikes.Where(l => l.CommentId == commentId).Count(l => l.Like == false);
                var commentLikes = db.CommentsLikes.Where(l => l.CommentId == commentId).Count(l => l.Like);
                return Json(new
                {
                    commentId = commentId,
                    commentLikes = commentLikeCount,
                    commentLikeCount = commentLikes,
                    commentDislikeCount = commenDislikes
                });
                //return RedirectToAction("Index");
            }
            message = DbChangeMessageId.Error;

            return RedirectToAction("Index", new { Message = message });
        }
    }


    #region HelpMessages



    public enum DbChangeMessageId
    {
        AddCommentSuccessfully,
        LikeSuccessfully,
        DislikeSuccessfully,
        Error
    }
    #endregion
}