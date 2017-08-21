using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class RegistroNotificacionesController : ApiController
    {
        public RegistroNotificaciones Get(int id)
        {
            return new RegistroNotificaciones().ObtenerRegistro(id);
        }

        public bool Post(RegistroNotificaciones Registro)
        {
            return new RegistroNotificaciones().AgregarRegistro(Registro);
        }

        public bool Delete(int id)
        {
            return new RegistroNotificaciones().EliminarRegistro(id);
        }
    }
}
