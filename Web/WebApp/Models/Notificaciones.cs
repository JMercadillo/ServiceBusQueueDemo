using System;

namespace WebApp.Models
{
    public class Notificaciones
    {
        public int NotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime? AgregadoEn { get; set; }
        public bool Leido { get; set; }
    }
}