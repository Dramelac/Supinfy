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
using System.IO;
using Newtonsoft.Json;

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

        public ActionResult UserPlaylist(Guid friendId)
        {
            if (Session[SessionKey.UserId] == null) return RedirectToAction("Login", "Account");

            var friend = UserDAO.GetUser(friendId);
            if (friend == null) return RedirectToAction("Index");
            var user = UserDAO.GetUser((Guid)Session[SessionKey.UserId]);
            if (user.Friends.Contains(friend) && friend.Friends.Contains(user))
            {
                var vm = PlaylistIndexVM.ToVM(friend.Playlists);
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
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
                    if (vm.OwnerName == user.Nickname) vm.IsOwner = true;
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
                    Music music = MusicDAO.GetMusicByTrackId(trackId);
                    if (music == null)
                    {
                        string url = "https://api.jamendo.com/v3.0/tracks?client_id=ab5d1fe6&id=" + trackId.ToString();
                        HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
                        r.Method = "GET";
                        HttpWebResponse res = (HttpWebResponse)r.GetResponse();
                        Stream sr = res.GetResponseStream();
                        StreamReader sre = new StreamReader(sr);
                        string s = sre.ReadToEnd();
                        MusicSearchVM data = JsonConvert.DeserializeObject<MusicSearchVM>(s);
                        if (data.Headers.code == 0)
                        {
                            if (data.Headers.results_count > 0)
                            {
                                MusicAPIVM result = data.Results[0];
                                Music newMusic = new Music();
                                newMusic.Artist = result.artist_name;
                                newMusic.Title = result.name;
                                newMusic.TrackId = result.id;
                                newMusic.TrackUrl = result.audio;
                                newMusic.ArtworkUrl = result.image;
                                MusicDAO.AddMusic(newMusic);
                            }
                            else
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                            }
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                    }

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
        
        public ActionResult RemoveMusic(Guid playlistId, int trackId)
        {
            if (Session[SessionKey.UserId] != null)
            {
                var playlist = PlaylistDAO.GetPlaylist(playlistId);
                if (playlist != null && playlist.OwnerId == (Guid)Session[SessionKey.UserId])
                {
                    if (PlaylistDAO.RemoveMusicFromPlaylist(playlistId, trackId))
                    {
                        return RedirectToAction("Detail", new {id = playlistId});
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
    }
}