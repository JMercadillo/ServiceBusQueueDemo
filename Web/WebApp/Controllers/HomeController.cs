using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Models;
using WebApp.Notifications;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Por favor inicie sesión.";

            return View();
        }

        public int GETUSERID()
        {
            return ((Usuario)Session["Usuario"]).UsuarioId;
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            if(new UsuariosHelper().Post(usuario))
            {
                var user = new UsuariosHelper().Get(usuario);
                Session["Usuario"] = user;

                NotificationComponent NC = new NotificationComponent();
                Session["LastUpdated"] = DateTime.Now;
                NC.RegisterNotification(DateTime.Now, user);

                return RedirectToAction("Calculadora");
            }

            return View();
        }

        public ActionResult Calculadora()
        {
            var user = Session["Usuario"] as Usuario;
            ViewBag.Message = $"{user.Nombre} ingresa unos números.";

            return View();
        }

        public ActionResult Resultado(Calculo calculo)
        {
            int result = 0;

            var calHelper = new CalculadoraHelper();

            var user = Session["Usuario"] as Usuario;
            ViewBag.Message = $"{user.Nombre} tu resultado está listo.";

            calculo.Usuario = user;
            //Envia el mensaje a la cola
            result = calHelper.EnviarOperar(calculo);

            if (result > 0)
                result = calHelper.Get();

            if (result > 0)
            {
                return View(model: "En breve caerá una notificación indicandote el resultado de tu operación.");
            }
            else
                return View();
        }

        public int GetNotificationsNotRead()
        {
            var usuario = Session["Usuario"] as Usuario;
            NotificacionHelper NH = new NotificacionHelper();

            return NH.Get(usuario.UsuarioId);
        }

        public JsonResult GetNotifications()
        {
            //var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            var usuario = Session["Usuario"] as Usuario;

            NotificacionHelper NH = new NotificacionHelper();
            var list = NH.GetAll(usuario.UsuarioId);

            Session["LastUpdated"] = DateTime.Now;

            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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