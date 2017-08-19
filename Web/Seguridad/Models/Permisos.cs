using Seguridad.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Models
{
    public class Permisos
    {
        public Permisos()
        {
            Rol = new List<Roles>();
        }

        public RolesPermisos PermisoId { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime CreadoEn { get; set; }

        public virtual ICollection<Roles> Rol { get; set; }
    }
}
