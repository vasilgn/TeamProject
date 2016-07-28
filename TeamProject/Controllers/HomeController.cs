using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DataModels;
using System.Data.Entity;
using System.Web.Mvc;

namespace TeamProject.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
			
            return View();
        }
    }
}