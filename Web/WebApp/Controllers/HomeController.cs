using Seguridad.Common;
using Seguridad.Helpers;
using System;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Models;
using WebApp.Notifications;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            ViewBag.Message = $"Bienvenid@ {SessionHelper.CurrentUser.Nombre}";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}