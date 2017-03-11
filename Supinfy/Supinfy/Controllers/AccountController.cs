using Supinfy.DAL;
using Supinfy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supinfy.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM form)
        {
            if (ModelState.IsValid && UserDAO.CheckAuth(form))
            {
                Session["UserMail"] = form.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(UserVM form)
        {
            if (ModelState.IsValid && UserDAO.AddUser(form))
            {
                Session["UserMail"] = form.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["UserMail"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}