using Seguridad.Helpers;
using Seguridad.Models;
using Seguridad.Notifications;
using System;
using System.Web;

namespace Seguridad.Common
{
    public class SessionHelper
    {
        private static string SessionUser = "Usuario";

        private SessionHelper()
        {
            // área para usuario no logueado
            Usuario = new Usuario() { UsuarioId = -1, Nombre = "Default", RolId = 0, Correo = "", };
            HoraIngreso = DateTime.Now;
        }

        public Usuario Usuario { get; set; }
        public DateTime HoraIngreso { get; set; }

        /// <summary>
        /// Inicia una sesión de usuario
        /// </summary>
        /// <param name="usuario">Usuario registrado</param>
        public static void StartSession(Usuario usuario)
        {
            var user = new Usuario().Obtener(usuario.UsuarioId);
            new BitacoraAccesoHelper().Post(
                new Bitacora()
                {
                    Accion = "E",
                    BitacoraIngresosId = 0,
                    Departamento = user.Rol.Nombre,
                    Fecha = DateTime.Now,
                    NombreCompleto = user.Nombre,
                    NombreUsuario = user.Nombre,
                    UsuarioId = user.UsuarioId.ToString(),
                    Terminal = System.Security.Principal.WindowsIdentity.GetCurrent().Name
                }
                );

            new NotificationComponent().RegisterNotification(DateTime.Now, user);

            HttpContext.Current.Session.Timeout = 480;
            HttpContext.Current.Session[SessionUser] = new SessionHelper() { Usuario = usuario, HoraIngreso = DateTime.Now };
        }

        /// <summary>
        /// Vuelve nula la sesión actual
        /// </summary>
        /// <param name="usuario">Usuario registrado</param>
        public static void EndSession()
        {
            var _user = HttpContext.Current.Session[SessionUser] as SessionHelper;
            var user = new Usuario().Obtener(_user.Usuario.UsuarioId);
            new BitacoraAccesoHelper().Post(
                new Bitacora()
                {
                    Accion = "S",
                    BitacoraIngresosId = 0,
                    Departamento = user.Rol.Nombre,
                    Fecha = DateTime.Now,
                    NombreCompleto = user.Nombre,
                    NombreUsuario = user.Nombre,
                    UsuarioId = user.UsuarioId.ToString(),
                    Terminal = System.Security.Principal.WindowsIdentity.GetCurrent().Name
                }
                );

            new NotificationComponent().RemoveNotification(user);

            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session[SessionUser] = null;
        }

        /// <summary>
        /// Obtiene la sesión actual
        /// </summary>
        public static SessionHelper Current
        {
            get
            {
                SessionHelper session =
                  (SessionHelper)HttpContext.Current.Session[SessionUser];
                if (session == null)
                {
                    session = new SessionHelper();
                    HttpContext.Current.Session[SessionUser] = session;
                }
                return session;
            }
        }

        /// <summary>
        /// Obtiene el usuario de la sesión actual
        /// </summary>
        public static Usuario CurrentUser
        {
            get
            {
                var usuario =
                  ((SessionHelper)HttpContext.Current.Session[SessionUser]).Usuario;
                if (usuario == null)
                {
                    var session = new SessionHelper();
                    HttpContext.Current.Session[SessionUser] = session;
                }
                return usuario;
            }
        }

        /// <summary>
        /// Obtiene el usuario de la sesión actual
        /// </summary>
        public static bool ExistUserInSession()
        {
            return HttpContext.Current.Session[SessionUser] != null;
        }
    }
}
