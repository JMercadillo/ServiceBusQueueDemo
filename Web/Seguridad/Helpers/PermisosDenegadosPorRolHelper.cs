using Seguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Helpers
{
    public class PermisosDenegadosPorRolHelper
    {
        String Uri = "http://localhost:57607/api/PermisosDenegadosPorRol/";

        public List<PermisosDenegadosPorRol> Get(int RolId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + RolId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<List<PermisosDenegadosPorRol>>().Result;
                    return result;
                }
                else
                {
                    return new List<PermisosDenegadosPorRol>();
                }
            }
        }
    }
}
