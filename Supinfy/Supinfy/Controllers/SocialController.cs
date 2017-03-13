using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supinfy.DAL;
using Supinfy.Utils;
using Supinfy.ViewModel;

namespace Supinfy.Controllers
{
    public class SocialController : Controller
    {
        // GET: Friend
        public ActionResult Index()
        {
            if (Session[SessionKey.UserId] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult SearchUser(string search)
        {
            if (Session[SessionKey.UserId] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var resultList = UserDAO.UserSearch(search);
            var vm = UserSearchResultVM.ToVM(resultList);
            return View(vm);
        }

        public ActionResult AddToFiend(Guid friendId)
        {
            return View();
        }
    }
}