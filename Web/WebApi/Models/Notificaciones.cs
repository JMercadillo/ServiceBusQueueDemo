using System;
using System.Collections.Generic;
using System.Linq;

using WebApi.DataContext;

namespace WebApi.Models
{
    public class Notificaciones
    {
        private int NotificacionId { get; set; }
        private int UsuarioId { get; set; }
        private string Titulo { get; set; }
        private string Cuerpo { get; set; }
        private DateTime? AgregadoEn { get; set; }
        private bool Leido { get; set; }

        public Notificaciones()
        {

        }

        public Notificaciones(int notificacionId, int usuarioId, string titulo, string cuerpo, DateTime? agregadoEn, bool leido)
        {
            NotificacionId = notificacionId;
            UsuarioId = usuarioId;
            Titulo = titulo;
            Cuerpo = cuerpo;
            AgregadoEn = agregadoEn;
            Leido = leido;
        }

        private DataContext.Notificaciones RetornaContexto(Notificaciones Obj)
        {
            return new DataContext.Notificaciones()
            {
                AgregadoEn = Obj.AgregadoEn,
                Cuerpo = Obj.Cuerpo,
                Leido = Obj.Leido,
                NotificacionId = Obj.NotificacionId,
                Titulo = Obj.Titulo,
                UsuarioId = Obj.UsuarioId
            };
        }

        private Notificaciones RetornaContexto(DataContext.Notificaciones Obj)
        {
            return new Notificaciones()
            {

                AgregadoEn = Obj.AgregadoEn,
                Cuerpo = Obj.Cuerpo,
                Leido = Obj.Leido,
                NotificacionId = Obj.NotificacionId,
                Titulo = Obj.Titulo,
                UsuarioId = Obj.UsuarioId
            };
        }

        public bool AgregarNotificacion(Notificaciones notificacion)
        {
            try
            {
                using (var context = new NotificationsDemoEntities())
                {
                    context.Notificaciones.Add(RetornaContexto(notificacion));

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Notificaciones> TodasNotificacionesPorUsuarioId(int UsuarioId)
        {
            using (var context = new NotificationsDemoEntities())
            {
                var lista = (from n in context.Notificaciones
                             where n.UsuarioId == UsuarioId && n.Leido == false
                             select n).ToList();
                var result = new List<Notificaciones>();
                foreach (var noti in lista)
                {
                    result.Add(RetornaContexto(noti));
                }
                LeerNotificaciones(UsuarioId);
                return result;
            }
        }

        private void LeerNotificaciones(int usuarioId)
        {
            using (DataContext.NotificationsDemoEntities model = new DataContext.NotificationsDemoEntities())
            {
                var notificaciones = model.Notificaciones
                    .Where(x => /*x.Leido == false &&*/ x.UsuarioId == usuarioId /*&& x.AgregadoEn > afterDate*/);

                foreach (var item in notificaciones)
                {
                    item.Leido = true;
                }
                model.SaveChanges();
            }
        }

        public bool EliminarNotificacion(int UsuarioId)
        {
            using (DataContext.NotificationsDemoEntities model = new DataContext.NotificationsDemoEntities())
            {
                var notificaciones = model.Notificaciones
                    .Where(x => x.UsuarioId == UsuarioId && x.Leido == true);

                model.Notificaciones.RemoveRange(notificaciones);

                return model.SaveChanges() > 0;
            }
        }

        public int CantidadNotificacionesPorUsuarioId(int UsuarioId)
        {
            using (var context = new DataContext.NotificationsDemoEntities())
            {
                return context.Notificaciones.Where(x => x.UsuarioId == UsuarioId).Count();
            }
        }
    }
}