using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TeamProject.DataModels;

namespace TeamProject.Controllers
{
    using System.Web.Mvc;
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        protected BlogDbContextEntities db = new BlogDbContextEntities();

        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }

    }
}