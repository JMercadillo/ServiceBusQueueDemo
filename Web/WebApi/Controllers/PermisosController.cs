using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class PermisosController : ApiController
    {
        // GET: api/Permisos
        public List<Permisos> Get()
        {
            return new Permisos().TodosPermisos();
        }

        // GET: api/Permisos/5
        public Permisos Get(int id)
        {
            return new Permisos().PermisoPorId(id);
        }

        //// POST: api/Permisos
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Permisos/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Permisos/5
        //public void Delete(int id)
        //{
        //}
    }
}
