namespace TeamProject.Controllers
{
    using System.Web.Mvc;
    using TeamProject.DataModels;

    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
    }
}