using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApi.Models
{
    [DataContract(Name = "Usuario", Namespace = "ServiceBusDemo")]
    public class Usuario
    {
        [DataMember]
        public int UsuarioId { get; set; }
        [DataMember]
        public int RolId { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Correo { get; set; }
        [DataMember]
        public DateTime CreadoEn { get; set; }

        private Usuario RetornaContexto(DataContext.Usuarios Obj)
        {
            return new Usuario()
            {
                Correo = Obj.Correo,
                CreadoEn = Obj.CreadoEn,
                Nombre = Obj.Nombre,
                RolId = Obj.RolId,
                UsuarioId = Obj.UsuarioId
            };
        }

        public bool Validar(Usuario usuario)
        {
            using (var context = new DataContext.NotificationsDemoEntities())
            {
                var users = context.Usuarios.Where(x => x.Nombre == usuario.Nombre && x.Correo == usuario.Correo);

                if (users.Count() == 1)
                    return true;
                else
                    return false;
            }
        }

        public Usuario ObtenerUsuario(Usuario usuario)
        {
            using (var context = new DataContext.NotificationsDemoEntities())
            {
                var result = context.Usuarios.Where(x => x.Nombre == usuario.Nombre && x.Correo == usuario.Correo).FirstOrDefault();

                return RetornaContexto(result);
            }
        }
    }
}