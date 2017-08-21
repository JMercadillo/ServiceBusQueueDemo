using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class PermisosDenegadosPorRol
    {
        public int PermisoDenegadoPorRolId { get; set; }
        public int RolId { get; set; }
        public int PermisoId { get; set; }
        public System.DateTime CreadoEn { get; set; }

        private PermisosDenegadosPorRol RetornarContexto(DataContext.PermisosDenegadosPorRol Obj)
        {
            return new PermisosDenegadosPorRol()
            {
                PermisoDenegadoPorRolId = Obj.PermisoDenegadoPorRolId,
                PermisoId = Obj.PermisoId,
                CreadoEn = Obj.CreadoEn,
                RolId = Obj.RolId
            };
        }

        public List<PermisosDenegadosPorRol> TodosPermisosDenegadosPorRol(int RolId)
        {
            using (var model = new DataContext.NotificationsDemoEntities())
            {
                var lista = (from r in model.PermisosDenegadosPorRol where r.RolId == RolId select r).ToList();
                var result = new List<PermisosDenegadosPorRol>();
                foreach (var obj in lista)
                {
                    result.Add(RetornarContexto(obj));
                }
                return result;
            }
        }
    }
}