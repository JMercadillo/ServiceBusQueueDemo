using Seguridad.Helpers;
using Seguridad.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Notifications
{
    public class NotificationComponent
    {

        Usuario USER = null;

        public void RegisterNotification(DateTime currentTime, Usuario usuario)
        {
            USER = usuario == null ? USER : usuario;

            if (USER != null)
            {

                var ConnString = ConfigurationManager.ConnectionStrings["NotificationsConnectionString"].ConnectionString;
                var query = @"SELECT [NotificacionId] FROM [dbo].[Notificaciones]" +
                    " WHERE [UsuarioId] = @UsuarioId AND [AgregadoEn] > @AgregadoEn";

                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@AgregadoEn", currentTime);
                    cmd.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);

                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.Notification = null;

                    SqlDependency sqlDep = new SqlDependency(cmd);
                    sqlDep.OnChange += SqlDep_OnChange;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
                var SH = new SubscriptionsHelper();
                var Subs = SH.Get(); // Obtiene la suscripción actual de dependencia con la base de datos
                var RH = new RegistroNotificacionesHelper();
                RH.Post( // Agrega el registro para controlar las suscripciones
                    new RegistroNotificaciones()
                    {
                        RegistroNotificacionId = 0,
                        SuscripcionId = Subs,
                        UsuarioId = USER.UsuarioId
                    }
                );
            }
        }

        /// <summary>
        /// Quita la dependencia de notifiaciones hacia la base de datos
        /// </summary>
        /// <param name="Usuario">Identificador del usuario</param>
        public void RemoveNotification(Usuario Usuario)
        {
            var RH = new RegistroNotificacionesHelper();
            var RegistroNoti = RH.Get(Usuario.UsuarioId);
            if (RegistroNoti != null)
            {
                var SH = new SubscriptionsHelper();
                SH.Delete(RegistroNoti.SuscripcionId); // Mata la suscripción de dependecia con la base de datos
                RH.Delete(Usuario.UsuarioId);
            }
        }

        private void SqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= SqlDep_OnChange;

                // Eliminando el registro de dependecia
                new RegistroNotificacionesHelper().Delete(USER.UsuarioId);

                if (USER != null)
                {
                    //notificando al cliente
                    new NotificationHub().AddNotification(USER);

                    RegisterNotification(DateTime.Now, USER);
                }
            }
        }
    }
}
