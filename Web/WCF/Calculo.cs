using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCF
{
    [DataContract(Name = "Calculo", Namespace = "ServiceBusDemo")]
    public class Calculo
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public int Numero1 { get; set; }
        [DataMember]
        public int Numero2 { get; set; }
        [DataMember]
        public int Operador { get; set; }
        [DataMember]
        public Usuario Usuario { get; set; }
    }
}