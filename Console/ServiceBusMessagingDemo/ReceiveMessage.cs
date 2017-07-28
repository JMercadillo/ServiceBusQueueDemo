using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Runtime.Serialization;

namespace ServiceBusMessagingDemo
{
    public class ReceiveMessage
    {
        //Obteniendo el cliente de la cola
        private QueueClient queueClient;
        private const string MessageType = "MessageType";
        private const string AssemblyName = "AssemblyName";

        public async Task Main(QueueClient _queueClient)
        {
            queueClient = _queueClient;
            Console.WriteLine($"{ DateTime.Now } > Ingrese la cantidad de mensajes a recibir:");
            Console.Write($"{DateTime.Now} > ");
            var strg = Console.ReadLine();
            var numReceiveMessage = 0;

            if( int.TryParse(strg, out numReceiveMessage) )
            {
                await ReceiveMessages(numReceiveMessage);
            }
            else
            {
                Console.WriteLine($"{ DateTime.Now } > Valor ingresado no valido.");
            }
        }

        private async Task ReceiveMessages(int numReceiveMessages)
        {
            try
            {
                for (int i = 0; i < numReceiveMessages; i++)
                {
                    
                    // llamando al mensaje
                    var brockeredMessage = await queueClient.ReceiveAsync();
                    Console.WriteLine($"{ DateTime.Now } > Mensaje recibido: { brockeredMessage.MessageId }, estado: { brockeredMessage.State }");
                    await brockeredMessage.CompleteAsync();
                    Console.WriteLine($"{ DateTime.Now } > Mensaje completado.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} > Excepción: {exception.Message}");
            }
        }

    }
}
