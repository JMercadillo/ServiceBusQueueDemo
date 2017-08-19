using Seguridad.Models;
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
            HttpContext.Current.Session.Timeout = 99999;
            HttpContext.Current.Session[SessionUser] = new SessionHelper() { Usuario = usuario, HoraIngreso = DateTime.Now };
        }

        /// <summary>
        /// Inicia una sesión por defecto
        /// </summary>
        /// <param name="usuario">Usuario registrado</param>
        public static void StartSession()
        {
            HttpContext.Current.Session.Timeout = 99999;
            HttpContext.Current.Session[SessionUser] = new SessionHelper();
        }

        /// <summary>
        /// Vuelve nula la sesión actual
        /// </summary>
        /// <param name="usuario">Usuario registrado</param>
        public static void EndSession()
        {
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
