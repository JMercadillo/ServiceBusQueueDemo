using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class PermisosDenegadosPorRolController : ApiController
    {
        // GET: api/PermisosDenegadosPorRol
        public List<PermisosDenegadosPorRol> Get(int id)
        {
            return new PermisosDenegadosPorRol().TodosPermisosDenegadosPorRol(id);
        }

        //// GET: api/PermisosDenegadosPorRol/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/PermisosDenegadosPorRol
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/PermisosDenegadosPorRol/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/PermisosDenegadosPorRol/5
        //public void Delete(int id)
        //{
        //}
    }
}
