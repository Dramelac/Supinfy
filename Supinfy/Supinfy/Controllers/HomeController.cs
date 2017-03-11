using Supinfy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supinfy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserDAO.AddUser(new Models.User
            {
                Email = "toto@toto.com",
                Name = "toto",
                Nickname = "toto",
                Password = ""
            });
            var test = UserDAO.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}