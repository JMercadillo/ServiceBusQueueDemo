using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApi.DataContext;

namespace WebApi.Models
{
    public class Notificacion
    {
        public int NotificacionId { get; set; }
        public int Usuario { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime AgregadoEn { get; set; }

        public int EnviarNotificacion(Notificacion notificacion, Usuario usuario)
        {
            try
            {
                using (NotificationsDemoEntities context = new NotificationsDemoEntities())
                {
                    Notificaciones notificaciones = new Notificaciones();
                    notificaciones.Leido = false;
                    notificaciones.Titulo = notificacion.Titulo;
                    notificaciones.Subtitulo = notificacion.Subtitulo;
                    notificaciones.Cuerpo = notificacion.Cuerpo;
                    notificaciones.AgregadoEn = DateTime.Now;
                    notificaciones.Usuario = usuario.Id;

                    context.Notificaciones.Add(notificaciones);

                    context.SaveChanges();
                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}