using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TeamProject.DataModels
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<PostLike> PostLikes { get; set; }
        public IDbSet<PostDislike> PostDislike { get; set; }
        public IDbSet<PostImage> PostImages { get; set; }
        public IDbSet<UserImage> UserImages { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}