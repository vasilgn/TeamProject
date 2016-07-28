using Microsoft.AspNet.Identity;
using TeamProject.DataModels;

namespace TeamProject.Controllers
{
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }
    }
}