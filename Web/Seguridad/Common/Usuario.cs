using Seguridad.Models;
using System;
using System.Runtime.Serialization;

namespace Seguridad.Common
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
        [IgnoreDataMember]
        public string Contrasenia { get; set; }
        [IgnoreDataMember]
        public Roles Rol { get; }
    }
}
