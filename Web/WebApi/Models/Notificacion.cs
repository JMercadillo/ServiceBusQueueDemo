using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}