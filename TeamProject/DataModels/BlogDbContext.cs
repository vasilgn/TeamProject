using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TeamProject.DataModels
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public BlogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
            // "BlogDbConnectionString"
        {
        }

        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<CommentLike> CommentsLikes { get; set; }
        public IDbSet<PostLike> PostLikes { get; set; }
        public IDbSet<PostImage> PostImages { get; set; }
        public IDbSet<PostVideo> PostVideos { get; set; }
        public IDbSet<UserImage> UserImages { get; set; }
        public virtual IDbSet<ChangeLog> ChangeLogs { get; set; }


        public static BlogDbContextEntities Create()
        {
            return new BlogDbContextEntities();
        }

       
    }

}