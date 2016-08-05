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
        public DateTime? Modified { get; set; }
        public int LikesCount { get; set; }
        public string Author { get; set; }

        public static Expression<Func<Post, PostViewModel>> ViewModel
        {
            get
            {
                return p => new PostViewModel()
                {
                    Id = p.PostId,
                    Title = p.Title,
                    Body = p.Body,
                    Modified = p.Modified,
                    PostDate = p.PostedOn,
                    LikesCount = p.PostLikeCounter,
                    Author = p.User.FullName,

                };
            }
        }
    }


    //Details View
    

}