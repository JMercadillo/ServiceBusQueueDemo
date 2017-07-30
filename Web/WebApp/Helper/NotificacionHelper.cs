using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebApp.Models;
using WebApp.Notifications;

namespace WebApp.Helper
{
    public class NotificacionHelper
    {

        public List<Notificacion> GetNotifications(bool Leer, int usuarioId/*DateTime afterDate*/)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //HTTP POST
                    var Uri = "http://localhost:57607/api/Notificaciones";

                    //var _afterDate = HttpUtility.UrlEncode(afterDate.ToString("yyyy/MM/dd hh:mm:ss"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync(Uri + $"?Leer={Leer}&usuarioId={usuarioId}"/*+$"?afterDate={_afterDate}"*/).Result;
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

        public int GetNotificacionsNotRead(int usuarioId)
        {
            var list = GetNotifications(false, usuarioId/*DateTime afterDate*/);
            var count = 0;
            foreach (var notifiacion in list)
            {
                count++;
            }

            return count;
        }
    }
}