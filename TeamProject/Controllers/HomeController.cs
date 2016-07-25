using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.DataModels;
using System.Data.Entity;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			
            return View();
        }

    }
}