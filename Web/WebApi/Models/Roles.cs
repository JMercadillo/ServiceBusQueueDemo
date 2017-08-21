using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class Roles
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime CreadoEn { get; set; }
        public List<PermisosDenegadosPorRol> PermisosDenegadosPorRol { get; set; }

        private Roles RetornarContexto(DataContext.Roles Obj)
        {
            return new Roles()
            {
                 CreadoEn = Obj.CreadoEn,
                 Descripcion = Obj.Descripcion,
                 Nombre = Obj.Nombre,
                 RolId = Obj.RolId
            };
        }

        public List<Roles> TodosRoles()
        {
            using (var model = new DataContext.NotificationsDemoEntities())
            {
                var lista = (from r in model.Roles select r).ToList();
                var result = new List<Roles>();
                foreach (var obj in lista)
                {
                    result.Add(RetornarContexto(obj));
                }
                return result;
            }
        }

        public Roles RolePorId(int RolId)
        {
            using (var model = new DataContext.NotificationsDemoEntities())
            {
                var obj = (from r in model.Roles where r.RolId == RolId select r).FirstOrDefault();
                return RetornarContexto(obj);
            }
        }
    }
}