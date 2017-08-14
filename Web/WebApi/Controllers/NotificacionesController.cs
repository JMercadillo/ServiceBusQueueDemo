using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class NotificacionesController : ApiController
    {
        [HttpGet]
        [Route("api/Notificaciones/GetAll/{id}")]
        public List<Notificaciones> GetAll(int id)
        {
            return new Notificaciones().TodasNotificacionesPorUsuarioId(id);
        }

        public int Get(int id)
        {
            return new Notificaciones().CantidadNotificacionesPorUsuarioId(id);
        }

        public bool Post(Notificaciones Notificacion)
        {
            return new Notificaciones().AgregarNotificacion(Notificacion);
        }

        public bool Delete(int id)
        {
            return new Notificaciones().EliminarNotificacion(id);
        }
    }
}
