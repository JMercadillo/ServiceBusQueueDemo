using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebApp.Notifications;

namespace WebApp
{
    public class NotificationComponent
    {
        public void RegisterNotification(DateTime currentTime)
        {
            var ConnString = ConfigurationManager.ConnectionStrings["NotificationsConnectionString"].ConnectionString;
            var query = @"SELECT [NotificacionId], [Usuario], [Titulo], [Cuerpo] FROM [dbo].[Notificaciones] WHERE [AgregadoEn] > @AgregadoEn";

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AgregadoEn", currentTime);
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

                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

                //notificando a los clientes
                notificationHub.Clients.All.notify("added");


                RegisterNotification(DateTime.Now);
            }
        }

        public List<Notificacion> GetNotifications(bool Leer/*DateTime afterDate*/)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //HTTP POST
                    var Uri = "http://localhost:57607/api/Notificaciones";
                    
                    //var _afterDate = HttpUtility.UrlEncode(afterDate.ToString("yyyy/MM/dd hh:mm:ss"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync(Uri+$"?Leer={Leer}"/*+$"?afterDate={_afterDate}"*/).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsAsync<List<Notificacion>>().Result;
                        return result;
                    }
                    else
                    {
                        return new List<Notificacion>();
                    }
                }
            }
            catch
            {
                return new List<Notificacion>();
            }
        }

        public int GetNotificacionsNotRead()
        {
            var list = GetNotifications(false/*DateTime afterDate*/);
            var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var count = 0;
            foreach (var notifiacion in list)
            {
                count++;
            }

            return count;
        }
    }
}