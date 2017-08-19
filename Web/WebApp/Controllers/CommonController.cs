using Seguridad.Common;
using Seguridad.Helpers;
using Seguridad.Models;
using System;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Notifications;
using WebApp.Security;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        [NoAuthenticate]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Common
        [Authenticate]
        public ActionResult Logout()
        {
            SessionHelper.EndSession();

            return RedirectToAction("Login", "Common", null);
        }

        [HttpPost]
        [NoAuthenticate]
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

        [Authenticate]
        public int GetNotificationsNotRead()
        {
            var usuario = SessionHelper.CurrentUser;
            NotificacionHelper NH = new NotificacionHelper();

            return NH.Get(usuario.UsuarioId);
        }

        [Authenticate]
        public JsonResult GetNotifications()
        {
            var usuario = SessionHelper.CurrentUser;

            NotificacionHelper NH = new NotificacionHelper();
            var list = NH.GetAll(usuario.UsuarioId);

            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authenticate]
        public int GetUserId()
        {
            return SessionHelper.CurrentUser.UsuarioId;
        }
    }
}