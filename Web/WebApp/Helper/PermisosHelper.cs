using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebApp.Models;

namespace WebApp.Helper
{
    public class PermisosHelper
    {
        String Uri = "http://localhost:57607/api/Permisos/";

        public Permisos Get(int PermisoId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + PermisoId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<Permisos>().Result;
                    return result;
                }
                else
                {
                    return new Permisos();
                }
            }
        }

        public List<Permisos> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<List<Permisos>>().Result;
                    return result;
                }
                else
                {
                    return new List<Permisos>();
                }
            }
        }
    }
}