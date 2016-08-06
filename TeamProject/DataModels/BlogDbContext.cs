using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TeamProject.DataModels
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<CommentLike> CommentsLikes { get; set; }
        public IDbSet<PostLike> PostLikes { get; set; }
        public IDbSet<PostImage> PostImages { get; set; }
        public IDbSet<PostVideo> PostVideos { get; set; }
        public IDbSet<UserImage> UserImages { get; set; }

        public BlogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }

    }
}