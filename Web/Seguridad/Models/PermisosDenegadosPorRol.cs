namespace Seguridad.Models
{
    public class PermisosDenegadosPorRol
    {
        public int PermisoDenegadoPorRolId { get; set; }
        public int RolId { get; set; }
        public int PermisoId { get; set; }
        public System.DateTime CreadoEn { get; set; }
    }
}
