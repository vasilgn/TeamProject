using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TeamProject.Extensions;
using TeamProject.Helpers;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            var posts = this.db.Posts.OrderBy(p => p.PostedOn)
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

            var isOwner = (postDetails?.UserId != null && postDetails.UserId == currentUserId);
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

        [HttpGet]
        public ActionResult GetNotifications()
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)

            ? (List<Alert>)TempData[Alert.TempDataKey]

            : new List<Alert>();
            
            var jsonResult = Json(new
            {
                alerts = alerts,
                size = alerts.Count()
            }, JsonRequestBehavior.AllowGet);
            
            return jsonResult;
        }
        //
        //POST Add comment
        [HttpPost]
        public ActionResult AddComment(CommentViewModel model, int id, string commentText)
        {
            var userId = this.User.Identity.GetUserId();
            var claim = ((ClaimsIdentity)User.Identity).FindFirst("FullName");
            if (model != null && ModelState.IsValid)
            {


                var newComment = new Comment()
                {
                    PostId = id,
                    CommentDate = DateTime.Now,
                    Text = commentText,
                    UserId = userId,
                    CommentLikeCounter = 0,

                };

                this.db.Comments.Add(newComment);
                this.db.SaveChanges();
                /*var thisComments = db.Comments.Where(c=>c.PostId== id).FirstOrDefault(c => c.CommentId== newComment.CommentId);*/

                Success($"You successfully add comment to {PostTitleById(id)} article.", true);

                var currentComment = db.Comments.Local[0].CommentId;
                var fullName = (claim != null) ? claim.Value : "Cant find Full name";


                var postId = newComment.PostId;
                var text = newComment.Text;
                var commentId = currentComment;

                var commentDate = (newComment.CommentDate).ToString();

                var data = new
                {
                    PostId = postId,
                    FullName = fullName,
                    Text = text,
                    CommentId = commentId,
                    CommentDate = commentDate,
                    UserName = this.User.Identity.GetUserName(),
                };

                return Json(new { model = data, JsonRequestBehavior.AllowGet });
            }
            Warning("Missing post or reference.");
            return RedirectToAction("Index");
        }

        //
        //POST Like
        

        [HttpPost]
        public ActionResult AddLike(PostViewModel model, int id, string command)
        {

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
                        if (isLike && command.Equals("Dislike."))
                        {
                            post.PostLikeCounter -= 2;
                            postLike.Like = false;
                            Success("Disliked.");
                        }
                        else if (!isLike && command.Equals("Like"))
                        {
                            post.PostLikeCounter += 2;
                            postLike.Like = true;
                            Success("Liked");
                        }

                        db.Entry(postLike).State = EntityState.Modified;
                        db.SaveChanges();
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
                            Information("You like this.");
                        }
                        else if (command.Equals("Dislike"))
                        {
                            post.PostLikeCounter -= 1;
                            newPostLike.Like = false;
                            Information("You didn't like this.");
                        }
                        db.PostLikes.Add(newPostLike);
                        db.SaveChanges();
                    }

                }

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                var postLikeCount = db.Posts.Find(id).PostLikeCounter;
                var postDislikes = db.PostLikes.Where(l => l.PostId == postId).Count(l => l.Like == false);
                var postLikes = db.PostLikes.Where(l => l.PostId == postId).Count(l => l.Like);
                return Json(new
                {
                    postId = postId,
                    postDislikeCount = postDislikes,
                    postLikeCount = postLikes,
                    postLikes = postLikeCount,
                });
                //return RedirectToAction("Index");
            }
            Error("Something went wrong try again.", true);
            return RedirectToAction("Index");
        }

        public ActionResult Notification(TempDataDictionary alert)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)

            ? (List<Alert>)TempData[Alert.TempDataKey]

            : new List<Alert>();


            return PartialView("_Alert", alert);
        }
       //
       //POST Comment Like
       [HttpPost]
        public ActionResult CommentLike(CommentViewModel model, int id, string command)
        {

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
                            Success("Disliked.", true);
                        }
                        else if (!isLike && command.Equals("Like"))
                        {
                            comment.CommentLikeCounter += 2;
                            commentLike.Like = true;
                            Success("Liked.", true);

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
                            Success("Liked.", true);

                        }
                        else if (command.Equals("Dislike"))
                        {
                            comment.CommentLikeCounter -= 1;
                            newCommentLike.Like = false;
                            Success("Disliked.", true);


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
            Warning("Error.", true);

            return RedirectToAction("Index");
        }
    }


}