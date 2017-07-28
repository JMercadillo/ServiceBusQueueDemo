using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
using System.Runtime.Serialization;

namespace ServiceBusMessagingDemo
{
    [DataContract]
    public class TestMessage
    {
        [DataMember]
        public Guid ExternalIdentifier { get; set; }
        [DataMember]
        public int Identifier { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
