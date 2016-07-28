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
        public IEnumerable<Comment> Comments { get; set; }
        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return c => new CommentViewModel()
                {
                    Text = c.Text,
                    Author = c.User.FullName
                };
            }
        }

        public partial class CommentDetailsViewModel
        {
            public DateTime CommentDate { get; set; }
            public string CommentId { get; set; }

        }
    }
}