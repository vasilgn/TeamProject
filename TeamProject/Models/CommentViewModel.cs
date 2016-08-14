using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TeamProject.DataModels;

namespace TeamProject.Models
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public int CommentId { get; set; }
        public string Username { get; set; }
        public DateTime CommentPostDate { get; set; }
        public int CommentCountLikes { get; set; }
        public int CommentLikes { get; set; }
        public int CommentDislikes { get; set; }
        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return c => new CommentViewModel()
                {
                    Text = c.Text,
                    CommentId = c.CommentId,
                    Author = c.User.FullName,
                    Username = c.User.UserName,
                    CommentPostDate = c.CommentDate,
                    CommentCountLikes = c.CommentLikeCounter,
                    CommentLikes = c.CommentLikes.Count(l => l.Like),
                    CommentDislikes = c.CommentLikes.Count(l => l.Like == false),
                };
            }
        }
    }
}