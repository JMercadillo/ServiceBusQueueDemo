using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApp.Models;

namespace WebApp.Helper
{
    public class NotificacionHelper
    {

        String Uri = "http://localhost:57607/api/Notificaciones/";

        public List<Notificaciones> GetAll(int UsuarioId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + $"GetAll/{UsuarioId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<List<Notificaciones>>().Result;
                    return result;
                }
                else
                {
                    return new List<Notificaciones>();
                }
            }
        }

        public int Get(int UsuarioId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + UsuarioId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<int>().Result;
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool Post(Notificaciones notificacion)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(Uri, notificacion).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<bool>().Result;
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Delete(int UsuarioId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync(Uri + UsuarioId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<bool>().Result;
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}