using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class Permisos
    {
        public int PermisoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime CreadoEn { get; set; }

        private Permisos RetornarContexto(DataContext.Permisos Obj)
        {
            return new Permisos()
            {
                CreadoEn = Obj.CreadoEn,
                Descripcion = Obj.Descripcion,
                Nombre = Obj.Nombre,
                PermisoId = Obj.PermisoId
            };
        }

        public List<Permisos> TodosPermisos()
        {
            using (var model = new DataContext.NotificationsDemoEntities())
            {
                var lista = (from r in model.Permisos select r).ToList();
                var result = new List<Permisos>();
                foreach (var obj in lista)
                {
                    result.Add(RetornarContexto(obj));
                }
                return result;
            }
        }

        public Permisos PermisoPorId(int PermisoId)
        {
            using (var model = new DataContext.NotificationsDemoEntities())
            {
                var obj = (from r in model.Permisos where r.PermisoId == PermisoId select r).FirstOrDefault();
                return RetornarContexto(obj);
            }
        }
    }
}