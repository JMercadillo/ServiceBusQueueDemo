using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DataContext;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class NotificacionesController : ApiController
    {
        public List<Notificaciones> Get(bool leer, int usuarioId/*DateTime afterDate*/)
        {
            var notificaciones = new List<Notificaciones>();
            using (DataContext.NotificationsDemoEntities model = new DataContext.NotificationsDemoEntities())
            {
                notificaciones = model.Notificaciones
                    .Where(x => x.Leido == false && x.Usuario == usuarioId)
                    .OrderByDescending(x => x.AgregadoEn)
                    .ToList();
                if (leer)
                {
                    foreach (var notificacion in notificaciones)
                    {
                        notificacion.Leido = true;
                    }
                    model.SaveChanges();
                }
            }

            return notificaciones;
        }
    }
}
