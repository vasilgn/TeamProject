using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TeamProject.DataModels;

namespace TeamProject.Models
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string AuthorId { get; set; }

        public static Expression<Func<Post, PostDetailsViewModel>> ViewModel
        {
            get
            {
                return e => new PostDetailsViewModel
                {
                    Id = e.PostId,
                    Description = e.Description,
                    ModifiedDate = e.Modified,
                    AuthorId = e.User.Id,

                };
            }
        }
    }
}