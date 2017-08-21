using Seguridad.Common;
using Seguridad.Helpers;
using Seguridad.Models;
using Seguridad.Notifications;
using System;
using System.Web.Mvc;
using WebApp.Helper;
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
                SessionHelper.StartSession(new UsuariosHelper().Get(usuario));

                return RedirectToAction("Index", "Home", null);
            }

            ModelState.AddModelError("", "El usuario o la contrasenia no coinciden.");
            return View();
        }

        [Authenticate]
        public int GetNotificationsNotRead()
        {
            return new NotificationHelper()
                .Get(SessionHelper.CurrentUser.UsuarioId);
        }

        [Authenticate]
        public JsonResult GetNotifications()
        {
            return new JsonResult { Data = new NotificationHelper()
                .GetAll(SessionHelper.CurrentUser.UsuarioId),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authenticate]
        public int GetUserId()
        {
            return SessionHelper.CurrentUser.UsuarioId;
        }
    }
}