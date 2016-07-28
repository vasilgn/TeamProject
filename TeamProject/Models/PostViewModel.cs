using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TeamProject.DataModels;

namespace TeamProject.Models
{
    //General Post view
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PostDate { get; set; }
        public string Author { get; set; }

        public static Expression<Func<Post, PostViewModel>> ViewModel
        {
            get
            {
                return p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    PostDate = p.PostedOn,
                    Author = p.User.FullName,

                };
            }
        }
    }


    //Details View
    public partial class PostDetailsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int PostLikeDislikeCounter { get; set; }
        public string AuthorId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public static Expression<Func<Post, PostDetailsViewModel>> ViewModel
        {
            get
            {
                return e => new PostDetailsViewModel
                {
                    Id = e.Id,
                    Description = e.Description,
                    ModifiedDate = e.Modified,
                    PostLikeDislikeCounter = e.PostLikeCounter,
                    AuthorId = e.User.Id,
                    Comments = e.Comments.AsQueryable().Select(CommentViewModel.ViewModel)

                };
            }
        }


    }

}