using System.Collections.Generic;
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

        // GET: api/Usuarios/5
        public Usuario Get(int id)
        {
            return new Usuario().ObtenerUsuario(id);
        }

        // GET: api/Usuarios/
        public List<Usuario> Get()
        {
            return new Usuario().ObtenerTodosUsuarios();
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
