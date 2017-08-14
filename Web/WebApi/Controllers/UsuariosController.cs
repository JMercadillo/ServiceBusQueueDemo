using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UsuariosController : ApiController
    {
        //// GET: api/Usuarios
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Usuarios/5
        public Usuario Get(string Correo, string Contrasenia)
        {
            var user = new Usuario() { Correo = Correo, Nombre = Contrasenia };
            return new Usuario().ObtenerUsuario(user);
        }

        // POST: api/Usuarios
        public bool Post(string Correo, string Contrasenia)
        {
            var user = new Usuario() { Correo = Correo, Nombre = Contrasenia };
            return new Usuario().Validar(user);
        }

        //// PUT: api/Usuarios/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Usuarios/5
        //public void Delete(int id)
        //{
        //}
    }
}
