using Seguridad.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Helpers
{
    public class UsuariosHelper
    {
        String Uri = "http://localhost:57607/api/Usuarios/";

        public bool Post(Usuario usuario)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(Uri + $"?Correo={usuario.Correo}&Contrasenia={usuario.Contrasenia}", null).Result;
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

        public Usuario Get(Usuario usuario)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + $"?Correo={usuario.Correo}&Contrasenia={usuario.Contrasenia}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<Usuario>().Result;
                    return result;
                }
                else
                {
                    return new Usuario();
                }
            }
        }
    }
}
