using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class SuscripcionesController : ApiController
    {
        public int Get()
        {
            return new Suscripciones().ConsultarSuscripcion();
        }
        public bool Delete(int id)
        {
            return new Suscripciones().EliminarSuscripcion(id);
        }
    }
}
