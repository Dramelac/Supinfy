using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supinfy.DAL;
using Supinfy.Models;
using Supinfy.Utils;
using Supinfy.ViewModel;

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
            var user = UserDAO.GetUser((Guid) Session[SessionKey.UserId]);
            var vm = PlaylistIndexVM.ToVM(user.Playlists);
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlaylistVM vm)
        {
            if (Session[SessionKey.UserId] != null)
            {
                var playlist = new Playlist
                {
                    Name = vm.Name,
                    CreatedDate = DateTime.Now,
                    OwnerId = (Guid)Session[SessionKey.UserId]
                };
                PlaylistDAO.CreatePlaylist(playlist);
            }

            return RedirectToAction("Index");
        }
    }
}