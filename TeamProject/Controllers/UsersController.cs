using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TeamProject.DataModels;
using TeamProject.Models;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace TeamProject.Controllers
	{
	public class UsersController : BaseController
		{
		public ActionResult Index(string sortOrder, string searchString)
			{
			ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "username" : "";
			ViewBag.FullNameSortParm = sortOrder == "username" ? "name" : "username";
			var users = from s in db.Users
						   select s;

			if (!String.IsNullOrEmpty(searchString))
				{
				users = users.Where(s => s.UserName.Contains(searchString) || s.FullName.Contains(searchString));
				}

			switch (sortOrder)
				{
				case "username":
					users = users.OrderBy(s => s.UserName);
					break;
				case "name":
					users = users.OrderBy(s => s.FullName);
					break;
				default:
					users = users.OrderBy(s => s.FullName);
					break;
				}
			return View(users.ToList());
			}


		//Details
		public ActionResult Details(int? id)
			{
			if (id == null)
				{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			UsersModel user = db.ApplicationUser.Find(id);
			if (user == null)
				{
				return HttpNotFound();
				}
			return View(user);
			}


		//Edit
		public ActionResult Edit(int? id)
			{
			if (id == null)
				{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			UsersModel user = db.ApplicationUser.Find(id);
			if (user == null)
				{
				return HttpNotFound();
				}
			return View(user);
			}

		//  User/Edit/
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public ActionResult EditPost(int? id)
			{
			if (id == null)
				{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			var userToUpdate = db.ApplicationUser.Find(id);
			if (TryUpdateModel(userToUpdate, "",
			   new string[] { "UserName", "FullName", "PasswordHash", "Email" }))
				{
				try
					{
					db.SaveChanges();

					return RedirectToAction("Index");
					}
				catch (RetryLimitExceededException /* dex */)
					{
					//Log the error (uncomment dex variable name and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
					}
				}
			return View(userToUpdate);
			}
	
	  // User/Delete
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
			UsersModel user = db.ApplicationUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                UsersModel user = db.ApplicationUser.Find(id);
				db.ApplicationUser.Remove(user);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }	
}
