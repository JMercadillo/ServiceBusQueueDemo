using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusMessagingDemo
{
    public class Log
    {
        private static string sbconnectionstring = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"].ToString();
        private static string QueueName = "";
        private static QueueClient queueClient;
        public static QueueClient CreateLog()
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(sbconnectionstring);
            Console.WriteLine($"{ DateTime.Now } > NamespaceManager creado correctamente.");

            var messagingFactory = MessagingFactory.CreateFromConnectionString(sbconnectionstring);
            Console.WriteLine($"{ DateTime.Now } > MessagingFactory creado correctamente.");

            Console.WriteLine($"{ DateTime.Now } > Verificando existencia de la cola.");

            if (namespaceManager.QueueExists(QueueName))
            {
                queueClient = messagingFactory.CreateQueueClient(QueueName, ReceiveMode.PeekLock);
                Console.WriteLine($"{ DateTime.Now } > QueueClient creado correctamente.");
            }
            else
            {
                Console.WriteLine($"{ DateTime.Now } > La cola no existe.");
            }

            return queueClient;
        }

        private static ServiceBusConnectionStringBuilder CreateConnectionString(int HttpPort, int TcpPort, string ServerFQDN, string ServiceNamespace, string sharedAccesskey, string sharedAccessName)
        {
            ServiceBusConnectionStringBuilder connBuilder = new ServiceBusConnectionStringBuilder();
            connBuilder.ManagementPort = HttpPort;
            connBuilder.RuntimePort = TcpPort;
            connBuilder.SharedAccessKey = sharedAccesskey;
            connBuilder.SharedAccessKeyName = sharedAccessName;
            connBuilder.Endpoints.Add(new UriBuilder() { Scheme = "sb", Host = ServerFQDN, Path = ServiceNamespace }.Uri);
            connBuilder.StsEndpoints.Add(new UriBuilder() { Scheme = "https", Host = ServerFQDN, Port = HttpPort, Path = ServiceNamespace }.Uri);

            return connBuilder;
        }
    }
}
