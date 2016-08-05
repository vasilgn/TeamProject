using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace TeamProject.Controllers
{
  
    public class PostController : BaseController
    {


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Post()
        {
            return PartialView("Posts");
        }



        #region Helpers



        #endregion


    }
}