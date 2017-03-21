using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
            var user = UserDAO.GetUser((Guid)Session[SessionKey.UserId]);
            var vm = FriendVM.ToVM(user);
            return View(vm);
        }

        public ActionResult SearchUser(string search)
        {
            if (Session[SessionKey.UserId] == null) return RedirectToAction("Login", "Account");

            if (search.IsNullOrWhiteSpace()) return RedirectToAction("Index");
            var resultList = UserDAO.UserSearch(search);
            resultList.Remove(resultList.FirstOrDefault(r => r.Id == (Guid)Session[SessionKey.UserId]));
            var vm = UserSearchResultVM.ToVM(resultList);
            return View(vm);
        }

        public ActionResult AddToFriend(Guid friendId, bool callback = true)
        {
            if (Session[SessionKey.UserId] == null) return RedirectToAction("Login", "Account");

            var friend = UserDAO.GetUser(friendId);
            if (friend == null) return RedirectToAction("Index");
            if (!UserDAO.AddPendingFriend((Guid) Session[SessionKey.UserId], friendId)) callback = false;
            return callback ? (ActionResult)View() : RedirectToAction("Index");
        }

        public ActionResult RemoveFriend(Guid friendId)
        {
            if (Session[SessionKey.UserId] == null) return RedirectToAction("Login", "Account");

            var friend = UserDAO.GetUser(friendId);
            if (friend == null) return RedirectToAction("Index");
            UserDAO.RemoveFriend((Guid)Session[SessionKey.UserId], friendId);
            return RedirectToAction("Index");
        }
    }
}