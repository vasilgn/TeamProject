using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.DataModels;

namespace TeamProject.Controllers
{
    public class UploadController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserImage upload, HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/Images"), guid + fileName);
                file.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                string imagepath = "~/Images/" + newpath;
                upload.ImageUrl = imagepath;
                db.UserImages.Add(upload);
                db.SaveChanges();
            }
            TempData["Success"] = "Upload successful";
            return RedirectToAction("Index", "Manage");
        }

    }
}