using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Models
{
    public class Notification
    {
        public int NotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime? AgregadoEn { get; set; }
        public bool Leido { get; set; }
    }
}
