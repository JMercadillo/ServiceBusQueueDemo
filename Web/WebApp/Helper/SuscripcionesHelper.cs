using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApp.Helper
{
    public class SuscripcionesHelper
    {
        String Uri = "http://localhost:57607/api/Suscripciones/";

        public int Get()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<int>().Result;
                    return result;
                }
                else
                {
                    return -1;
                }
            }
        }

        public bool Delete(int SuscripcionId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync(Uri + SuscripcionId).Result;
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