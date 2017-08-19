using Seguridad.Common;
using System.Collections.Generic;

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
