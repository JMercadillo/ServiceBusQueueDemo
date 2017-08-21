using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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
        [IgnoreDataMember]
        public string Contrasenia { get; set; }
        [DataMember]
        public DateTime CreadoEn { get; set; }
        [DataMember]
        public Roles Rol { get; set; }

        private Usuario RetornaContexto(DataContext.Usuarios Obj)
        {
            return new Usuario()
            {
                Correo = Obj.Correo,
                CreadoEn = Obj.CreadoEn,
                Nombre = Obj.Nombre,
                RolId = Obj.RolId,
                UsuarioId = Obj.UsuarioId,
            };
        }

        private Usuario RetornaContextoCompleto(DataContext.Usuarios Obj)
        {
            return new Usuario()
            {
                Correo = Obj.Correo,
                CreadoEn = Obj.CreadoEn,
                Nombre = Obj.Nombre,
                RolId = Obj.RolId,
                UsuarioId = Obj.UsuarioId,
                Rol = RetornarContextoRoles(Obj.Roles)
            };
        }

        private Roles RetornarContextoRoles(DataContext.Roles Obj)
        {
            return new Roles()
            { 
                 CreadoEn = Obj.CreadoEn,
                 Descripcion = Obj.Descripcion,
                 Nombre = Obj.Nombre,
                 PermisosDenegadosPorRol = Obj.PermisosDenegadosPorRol != null ? RetornarContextoPermisosDenegados(Obj.PermisosDenegadosPorRol) : null,
                 RolId = Obj.RolId
            };
        }

        private List<PermisosDenegadosPorRol> RetornarContextoPermisosDenegados(ICollection<DataContext.PermisosDenegadosPorRol> Obj)
        {
            var lista = new List<PermisosDenegadosPorRol>();
            foreach (var item in Obj)
            {
                lista.Add(RetornarContextoPDPR(item));
            }
            return lista;
        }

        private PermisosDenegadosPorRol RetornarContextoPDPR(DataContext.PermisosDenegadosPorRol Obj)
        {
            return new PermisosDenegadosPorRol(){
                 CreadoEn = Obj.CreadoEn,
                 PermisoDenegadoPorRolId = Obj.PermisoDenegadoPorRolId,
                 PermisoId = Obj.PermisoId,
                 RolId = Obj.RolId
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

        public Usuario ObtenerUsuario(int UsuarioId)
        {
            using (var context = new DataContext.NotificationsDemoEntities())
            {
                DataContext.Usuarios result = null;
                try
                {
                    result = context.Usuarios
                            .Include("Roles")
                            .Include("Roles.PermisosDenegadosPorRol")
                            .Where(x => x.UsuarioId == UsuarioId).FirstOrDefault();
                }
                catch
                {
                    result = new DataContext.Usuarios();
                }
                
                return RetornaContextoCompleto(result);
            }
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
            using (var context = new DataContext.NotificationsDemoEntities())
            {
                List<DataContext.Usuarios> lista = null;
                List<Usuario> result = null;
                try
                {
                    lista = context.Usuarios
                            .Include("Roles")
                            .ToList();

                    result = new List<Usuario>();
                }
                catch
                {
                    lista = new List<DataContext.Usuarios>();
                }

                foreach (var item in lista)
                {
                    result.Add(RetornaContextoCompleto(item));
                }

                return result;
            }
        }
    }
}