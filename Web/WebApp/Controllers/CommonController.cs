using Seguridad.Common;
using Seguridad.Helpers;
using System;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Notifications;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioViewModel _usuario)
        {
            var usuario = new Usuario() { Correo = _usuario.Correo, Contrasenia = _usuario.Contrasenia };
            if (new UsuariosHelper().Post(usuario))
            {
                var user = new UsuariosHelper().Get(usuario);

                SessionHelper.StartSession(user);

                NotificationComponent NC = new NotificationComponent();
                Session["LastUpdated"] = DateTime.Now;
                NC.RegisterNotification(DateTime.Now, user);

                return RedirectToAction("Index", "Home", null);
            }

            ModelState.AddModelError("", "El usuario o la contrasenia no coinciden.");
            return View();
        }


        public int GetNotificationsNotRead()
        {
            var usuario = SessionHelper.CurrentUser;
            NotificacionHelper NH = new NotificacionHelper();

            return NH.Get(usuario.UsuarioId);
        }

        public JsonResult GetNotifications()
        {
            var usuario = SessionHelper.CurrentUser;

            NotificacionHelper NH = new NotificacionHelper();
            var list = NH.GetAll(usuario.UsuarioId);

            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public int GetUserId()
        {
            return SessionHelper.CurrentUser.UsuarioId;
        }
    }
}