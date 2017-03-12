using Supinfy.DAL;
using Supinfy.Utils;
using Supinfy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Supinfy.Controllers
{
    public class AccountController : Controller
    {
        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM form)
        {
            if (ModelState.IsValid && UserDAO.CheckAuth(form))
            {
                Session[SessionKey.UserMail] = form.Email;
                Session[SessionKey.Username] = UserDAO.GetUsernameFromMail(form.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(UserVM form)
        {
            if (ModelState.IsValid && UserDAO.AddUser(form))
            {
                Session[SessionKey.UserMail] = form.Email;
                Session[SessionKey.Username] = form.NickName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        #endregion

        public ActionResult Logout()
        {
            Session[SessionKey.UserMail] = null;
            Session[SessionKey.Username] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile(string user)
        {
            var usr = UserDAO.GetUserFromUsername(user);
            if (usr == null)
            {
                return new HttpNotFoundResult();
            }

            var vm = ProfileVM.ModelToVm(usr);
            if (Session[SessionKey.Username] != null && Session[SessionKey.Username].ToString() == user)
            {
                vm.IsOwner = true;
            }
            return View(vm);
        }
    }
}