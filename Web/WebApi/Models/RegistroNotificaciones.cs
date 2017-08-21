using System.Linq;
using WebApi.DataContext;

namespace WebApi.Models
{
    public class RegistroNotificaciones
    {
        public int RegistroNotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public int SuscripcionId { get; set; }

        public RegistroNotificaciones RetornarContexto(DataContext.RegistroNotificaciones Obj)
        {
            return new RegistroNotificaciones()
            {
             RegistroNotificacionId = Obj.RegistroNotificacionId,
             SuscripcionId = Obj.SuscripcionId,
             UsuarioId = Obj.UsuarioId
            };
        }


        public bool AgregarRegistro(RegistroNotificaciones Registro)
        {
            using (var model = new NotificationsDemoEntities())
            {
                if (model.RegistroNotificaciones.Where(x =>
                 x.UsuarioId == Registro.UsuarioId)
                .Count() > 0)
                {
                    var registroExistente = model.RegistroNotificaciones.Where(x => x.UsuarioId == Registro.UsuarioId).FirstOrDefault();
                    if (registroExistente.SuscripcionId != Registro.SuscripcionId)
                    {
                        var registro = new DataContext.RegistroNotificaciones() { SuscripcionId = Registro.SuscripcionId, UsuarioId = Registro.UsuarioId };
                        model.RegistroNotificaciones.Add(registro);
                    }
                }
                else
                {
                    var registro = new DataContext.RegistroNotificaciones() { SuscripcionId = Registro.SuscripcionId, UsuarioId = Registro.UsuarioId };
                    model.RegistroNotificaciones.Add(registro);
                }
                return model.SaveChanges() > 0;
            }
        }

        public RegistroNotificaciones ObtenerRegistro(int UsuarioId)
        {
            using (var model = new NotificationsDemoEntities())
            {
                var result = model.RegistroNotificaciones.Where(x =>
                x.UsuarioId == UsuarioId).FirstOrDefault();
                return RetornarContexto(result);
            }
        }

        public bool EliminarRegistro(int UsuarioId)
        {
            using (var model = new NotificationsDemoEntities())
            {
                var registroExistente = model.RegistroNotificaciones.Where(x => x.UsuarioId == UsuarioId);
                model.RegistroNotificaciones.RemoveRange(registroExistente);
                return model.SaveChanges() > 0;
            }
        }
    }
}