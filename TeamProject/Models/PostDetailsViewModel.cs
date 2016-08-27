using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [DataType(DataType.Url)]
        public string VideoUrl { get; set; }
        public bool? IsPublic { get; set; }
        public static Expression<Func<Post, PostDetailsViewModel>> ViewModel
        {
            get
            {
                return e => new PostDetailsViewModel
                {
                    Id = e.PostId,
                    Title = e.Title,
                    Description = e.Description,
                    IsPublic = e.IsPublic,
                    UserId = e.User.Id,
                    Body = e.Body,
                    ImageUrl = e.PostImages.Select(s=>s.ImageUrl).FirstOrDefault(),
                    VideoUrl = e.PostVideos.Select(s => s.VideoUrl).FirstOrDefault(),
                    
                };
            }
        }
    }
}