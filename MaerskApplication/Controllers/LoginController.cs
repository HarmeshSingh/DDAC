using MaerskApplication.Models;
using MaerskApplication.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskApplication.Controllers
{
    public class LoginController : Controller
    {
        DataAccess da = new DataAccess();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginUser(LoginViewModel model)
        {
            var users = da.GetUser();

            var user = users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if(user != null)
            {
                Users.Authenticated = true;
                Users.CurrentUser = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Incorrect Username/Password";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Users.Authenticated = false;
            Users.CurrentUser = null;

            return RedirectToAction("Index", "Home");
        }
    }
}