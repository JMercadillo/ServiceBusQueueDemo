using Seguridad.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Seguridad.Helpers
{
    public class RolesHelper
    {
        String Uri = "http://localhost:57607/api/Roles/";

        public Roles Get(int RolId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + RolId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<Roles>().Result;
                    return result;
                }
                else
                {
                    return new Roles();
                }
            }
        }

        public List<Roles> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<List<Roles>>().Result;
                    return result;
                }
                else
                {
                    return new List<Roles>();
                }
            }
        }
    }
}
