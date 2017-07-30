using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        protected readonly List<Usuario> Usuarios = new List<Usuario>()
        {
              new Usuario(){ Id=0, Nombre = "Josué Ulises", Apellido = "Mercadillo Flores", Correo = "jmercadillo@cgclatam.com", Contrasenia = "abc123." },
              new Usuario(){ Id=1, Nombre = "Nataly Paola", Apellido = "Domínguez Landaverde", Correo = "metal_uli@hotmail.com", Contrasenia = "mercally05" },
        };

        public ActionResult Index()
        {
            ViewBag.Message = "Por favor inicie sesión.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            if (Usuarios.Where(x => x.Correo == usuario.Correo && x.Contrasenia == usuario.Contrasenia).Count() == 1)
            {
                var user = Usuarios.Where(x => x.Correo == usuario.Correo && x.Contrasenia == usuario.Contrasenia).First();
                Session["Usuario"] = user as Usuario;

                //Registrando al cliente para recibir notificaciones
                NotificationHub NH = new NotificationHub();
                NH.RegisterClient(user);

                return RedirectToAction("Calculadora");
            }

            if (Usuarios.Where(x => x.Correo == usuario.Correo).Count() < 1)
                ModelState.AddModelError("Correo", "Verifique su correo.");
            if (Usuarios.Where(x => x.Contrasenia == usuario.Contrasenia).Count() < 1)
                ModelState.AddModelError("Contrasenia", "Verifique su contraseña.");

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
                result = calHelper.GetOperar();

            if (result > 0)
                return View("En breve caerá una notificación indicandote el resultado de tu operación.");
            else
                return View();
        }

        public int GetNotificationsNotRead()
        {
            var usuario = Session["Usuario"] as Usuario;
            NotificacionHelper NH = new NotificacionHelper();
            return NH.GetNotificacionsNotRead(usuario.Id);
        }

        public JsonResult GetNotifications()
        {
            //var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            var usuario = Session["Usuario"] as Usuario;

            NotificacionHelper NH = new NotificacionHelper();
            var list = NH.GetNotifications(true, usuario.Id/*notificationRegisterTime*/);

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