using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supinfy.Utils;

namespace Supinfy.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        public ActionResult Index()
        {
            if (Session[SessionKey.UserId] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}