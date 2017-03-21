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
            if (playlist != null)
            {
                var user = UserDAO.GetUser((Guid)Session[SessionKey.UserId]);
                var playlistOwner = UserDAO.GetUser(playlist.OwnerId);
                if (user.Friends.Contains(playlistOwner) || user == playlistOwner)
                {
                    var vm = PlaylistVM.ToVM(playlist);
                    return View(vm);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
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
                    Musics = new List<Music>()
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

        [HttpPost]
        public ActionResult Ajax_ListPlaylist()
        {
            if (Session[SessionKey.UserId] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var user = UserDAO.GetUser((Guid)Session[SessionKey.UserId]);
            var vm = PlaylistIndexVM.ToVM(user.Playlists);
            return Json(new { playlists = vm.Playlists });
        }

        [HttpPost]
        public ActionResult Ajax_AddMusic(Guid playlistId, int trackId)
        {
            if (Session[SessionKey.UserId] != null)
            {
                var playlist = PlaylistDAO.GetPlaylist(playlistId);
                if (playlist != null && playlist.OwnerId == (Guid)Session[SessionKey.UserId])
                {
                    if (PlaylistDAO.AddMusicToPlaylist(playlistId, trackId))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

        [HttpPost]
        public ActionResult Ajax_RemoveMusic(Guid playlistId, int trackId)
        {
            if (Session[SessionKey.UserId] != null)
            {
                var playlist = PlaylistDAO.GetPlaylist(playlistId);
                if (playlist != null && playlist.OwnerId == (Guid)Session[SessionKey.UserId])
                {
                    if (PlaylistDAO.RemoveMusicFromPlaylist(playlistId, trackId))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
    }
}