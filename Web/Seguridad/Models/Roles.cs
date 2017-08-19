using Seguridad.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Models
{
    public class Roles
    {
        public Roles()
        {
            Usuario = new List<Usuario>();
            Permisos = new List<Permisos>();
        }

        private int RolId { get; set; }
        private string Nombre { get; set; }
        private string Descripcion { get; set; }
        private System.DateTime CreadoEn { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Permisos> Permisos { get; set; }
    }
}
