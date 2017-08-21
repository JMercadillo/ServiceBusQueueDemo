using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Models
{
    public class Bitacora
    {
        public int BitacoraIngresosId { get; set; }
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Accion { get; set; }
        public string Departamento { get; set; }
        public string NombreCompleto { get; set; }
        public string Terminal { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}
