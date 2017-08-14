using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RegistroNotificaciones
    {
        public int RegistroNotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public int SuscripcionId { get; set; }
    }
}