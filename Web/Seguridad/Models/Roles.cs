using System.Collections.Generic;

namespace Seguridad.Models
{
    public class Roles
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime CreadoEn { get; set; }
        public List<PermisosDenegadosPorRol> PermisosDenegadosPorRol { get; set; }
    }
}
