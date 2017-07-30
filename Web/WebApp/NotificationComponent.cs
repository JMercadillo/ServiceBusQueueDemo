using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebApp.Models;
using WebApp.Notifications;

namespace WebApp
{
    public class NotificationComponent
    {

        public void RegisterNotification(DateTime currentTime, Usuario usuario)
        {
            var ConnString = ConfigurationManager.ConnectionStrings["NotificationsConnectionString"].ConnectionString;
            var query = @"SELECT [NotificacionId], [Usuario], [Titulo], [Cuerpo] FROM [dbo].[Notificaciones]"+
                " WHERE [Usuario] = @UsuarioId AND [AgregadoEn] > @AgregadoEn";

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AgregadoEn", currentTime);
                cmd.Parameters.AddWithValue("@UsuarioId", usuario.Id);

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
        }

        private void SqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= SqlDep_OnChange;

                //var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                //notificando a los clientes
                //notificationHub.Clients.All.notify("added");

                var usuario = HttpContext.Current.Session["Usuario"] as Usuario;
                if (usuario != null)
                {
                    //notificando al cliente
                    NotificationHub NH = new NotificationHub();
                    NH.AddNotification(usuario);

                    RegisterNotification(DateTime.Now, usuario);
                }
            }
        }
    }
}