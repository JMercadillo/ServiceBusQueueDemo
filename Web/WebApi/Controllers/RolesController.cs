using System.Collections.Generic;
using System.Web.Http;

using WebApi.Models;
namespace WebApi.Controllers
{
    public class RolesController : ApiController
    {
        // GET: api/Roles
        public List<Roles> Get()
        {
            return new Roles().TodosRoles();
        }

        // GET: api/Roles/5
        public Roles Get(int id)
        {
            return new Roles().RolePorId(id);
        }

    //    // POST: api/Roles
    //    public void Post([FromBody]string value)
    //    {
    //    }

    //    // PUT: api/Roles/5
    //    public void Put(int id, [FromBody]string value)
    //    {
    //    }

    //    // DELETE: api/Roles/5
    //    public void Delete(int id)
    //    {
    //    }
    }
}
