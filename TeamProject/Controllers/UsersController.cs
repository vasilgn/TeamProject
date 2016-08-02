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
		}
}