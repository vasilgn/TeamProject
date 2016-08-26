using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;

namespace TeamProject.Models
{
    public class PostsViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public IEnumerable<PostVideo> PostVideos { get; set; }

    }

}