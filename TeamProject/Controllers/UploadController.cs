using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Controllers
{
    public class UploadController : BaseController
    {
        

        
        /*[HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                
            try
                {
                    
                    string path = Path.Combine(Server.MapPath("~/Content/images/posts"),
                        Path.GetFileName(file.FileName));
                    
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Create","Posts");
        }
    }*/
    }
}