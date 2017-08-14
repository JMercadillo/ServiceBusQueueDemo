using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Roles
    {
        private int RolId { get; set; }
        private string Nombre { get; set; }
        private string Descripcion { get; set; }
        private System.DateTime CreadoEn { get; set; }

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