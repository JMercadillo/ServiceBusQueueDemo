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
        public static string sbconnectionstring = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"].ToString();
        //private static string QueueName = "colademo";
        public static QueueClient queueClient;
        public static QueueClient CreateLog()
        {
            /*string*/ sbconnectionstring = CreateConnectionString(9355, 9354, "server", "", "5GiPut9Di6ZdGA+aQjHzTHho+E03bf8291on0Y0484Q=", "RootManageSharedAccessKey").ToString();
            try
            {
                queueClient = QueueClient.CreateFromConnectionString(sbconnectionstring, "colademo", ReceiveMode.ReceiveAndDelete);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectarse con la cola.", ex);
            }
            

            //Console.WriteLine($"{ DateTime.Now } > QueueClient creado exitosamente.");

            //var messagingFactory = MessagingFactory.CreateFromConnectionString(sbconnectionstring);
            //Console.WriteLine($"{ DateTime.Now } > MessagingFactory creado correctamente.");

            //Console.WriteLine($"{ DateTime.Now } > Verificando existencia de la cola.");

            //if (namespaceManager.QueueExists(QueueName))
            //{
            //    queueClient = messagingFactory.CreateQueueClient(QueueName, ReceiveMode.PeekLock);
            //    Console.WriteLine($"{ DateTime.Now } > QueueClient creado correctamente.");
            //}
            //else
            //{
            //    Console.WriteLine($"{ DateTime.Now } > La cola no existe.");
            //}

            return queueClient;
        }

        private static ServiceBusConnectionStringBuilder CreateConnectionString(int HttpPort, int TcpPort, string ServerFQDN, string ServiceNamespace, string sharedAccesskey, string sharedAccessName)
        {
            ServiceBusConnectionStringBuilder connBuilder = new ServiceBusConnectionStringBuilder();
            connBuilder.ManagementPort = HttpPort;
            connBuilder.RuntimePort = TcpPort;
            connBuilder.SharedAccessKey = sharedAccesskey;
            connBuilder.SharedAccessKeyName = sharedAccessName;
            connBuilder.Endpoints.Add(new UriBuilder() { Scheme = "sb", Host = ServerFQDN }.Uri);
            connBuilder.StsEndpoints.Add(new UriBuilder() { Scheme = "http", Host = ServerFQDN, Port = HttpPort }.Uri);

            return connBuilder;
        }
    }
}
