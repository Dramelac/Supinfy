using Supinfy.DAL;
using Supinfy.Utils;
using Supinfy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

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
                var user = UserDAO.GetUserFromMail(form.Email);
                Session[SessionKey.Username] = user.Nickname;
                Session[SessionKey.UserId] = user.Id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect authentication");
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
                var user = UserDAO.GetUserFromMail(form.Email);
                Session[SessionKey.Username] = form.NickName;
                Session[SessionKey.UserId] = user.Id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "This email or nickname already exists");
                return View();
            }
        }
        #endregion

        public ActionResult Logout()
        {
            Session[SessionKey.UserId] = null;
            Session[SessionKey.Username] = null;
            return RedirectToAction("Index", "Home");
        }

        #region Profile
        public ActionResult Profile(string user)
        {
            var usr = UserDAO.GetUserFromUsername(user);
            if (usr == null)
            {
                return new HttpNotFoundResult();
            }

            var vm = ProfileVM.ModelToVm(usr);
            if (Session[SessionKey.UserId] != null && Equals(Session[SessionKey.UserId], usr.Id))
            {
                vm.IsOwner = true;
            } else if (usr.Friends.Any(f => f.Id == (Guid) Session[SessionKey.UserId]))
            {
                vm.IsFriend = true;
            }
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Profile(ProfileVM vm)
        {
            if (Session[SessionKey.UserId] == null)
            {
                return new HttpNotFoundResult();
            }
            vm.Id = (Guid) Session[SessionKey.UserId];
            UserDAO.UpdateUser(vm);

            var resultvm = ProfileVM.ModelToVm(UserDAO.GetUser(vm.Id));
            resultvm.IsOwner = true;
            return View(resultvm);

        }
        #endregion
    }
}