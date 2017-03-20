using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Supinfy.DAL;
using Supinfy.Utils;
using Supinfy.ViewModel;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Supinfy.Controllers
{
    public class MusicController : Controller
    {
        // GET: Music
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SearchMusic(string search)
        {
            if (Session[SessionKey.UserId] == null) return RedirectToAction("Login", "Account");

            if (search == "") return RedirectToAction("Index", "Home");

            string url = "https://api.jamendo.com/v3.0/tracks?client_id=ab5d1fe6&limit=20&search=" + search;
            HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
            r.Method = "GET";
            HttpWebResponse res = (HttpWebResponse)r.GetResponse();
            Stream sr = res.GetResponseStream();
            StreamReader sre = new StreamReader(sr);

            string s = sre.ReadToEnd();
            MusicSearchVM data = JsonConvert.DeserializeObject<MusicSearchVM>(s);
            return View(data);
        }

    }
}