using Seguridad.Helpers;
using System;
using System.Runtime.Serialization;

namespace Seguridad.Models
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

        public Usuario Obtener(int id)
        {
            var usuario = new Usuario();

            try
            {
                usuario = new UsuariosHelper().Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return usuario;
        }
    }
}
