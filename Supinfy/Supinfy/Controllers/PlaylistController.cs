using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult Detail(Guid id)
        {
            if (Session[SessionKey.UserId] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var playlist = PlaylistDAO.GetPlaylist(id);
            if (playlist == null)
            {
                return RedirectToAction("Index");
            }

            var vm = PlaylistVM.ToVM(playlist);

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
                    CreatedDate = DateTime.Now
                };
                playlist.OwnerId = (Guid) Session[SessionKey.UserId];
                PlaylistDAO.CreatePlaylist(playlist);
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult Remove(Guid playlistId)
        {
            if (Session[SessionKey.UserId] != null)
            {
                var playlist = PlaylistDAO.GetPlaylist(playlistId);
                if (playlist != null && playlist.OwnerId == (Guid) Session[SessionKey.UserId])
                {
                    PlaylistDAO.RemovePlaylist(playlist);
                    //return new HttpStatusCodeResult(HttpStatusCode.OK);
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

        public ActionResult RemoveMusic(Guid playlistId, Guid musicId)
        {
            return RedirectToAction("Detail", new {id = playlistId});
        }

        public ActionResult Ajax_AddMusic(Guid playlistId, Guid musicId)
        {
            if (Session[SessionKey.UserId] != null)
            {
                var playlist = PlaylistDAO.GetPlaylist(playlistId);
                if (playlist != null && playlist.OwnerId == (Guid)Session[SessionKey.UserId])
                {
                    if (PlaylistDAO.AddMusicToPlaylist(playlistId, musicId))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
    }
}