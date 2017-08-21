using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Models
{
    public class RegistroNotificaciones
    {
        public int RegistroNotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public int SuscripcionId { get; set; }
    }
}
