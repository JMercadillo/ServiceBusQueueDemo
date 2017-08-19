using Seguridad.Common;
using System.Web.Mvc;
using WebApp.Security;

namespace WebApp.Controllers
{

    public class HomeController : Controller
    {
        [Authenticate]
        public ActionResult Index()
        {
            ViewBag.Message = $"Bienvenid@ {SessionHelper.CurrentUser.Nombre}";

            return View();
        }

        [NoAuthenticate]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [NoAuthenticate]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}