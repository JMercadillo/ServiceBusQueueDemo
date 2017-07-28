using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Espacios de nombres agregados
using Microsoft.ServiceBus.Messaging;
using System.Runtime.Serialization;

namespace ServiceBusMessagingDemo
{
    public class SendMessage
    {
        // Contexto de datos a enviar
        private const string MessageType = "MessageType";
        private const string AssemblyName = "AssemblyName";
        private QueueClient queueClient;

        public async Task Main(QueueClient _queueClient)
        {
            queueClient = _queueClient;
            Console.WriteLine($"{ DateTime.Now } > Ingrese la cantidad de mensajes a enviar a la cola: ");
            Console.Write($"{ DateTime.Now } > ");
            var strinsg = Console.ReadLine();
            var numMessagesToSend = 0;
            if (int.TryParse(strinsg, out numMessagesToSend))
            {
                // Enviando los mensajes
                await SendMessagesToQueue(numMessagesToSend);
                // Cerrando conexion
                await queueClient.CloseAsync();
            }
            else
            {
                Console.WriteLine($"{ DateTime.Now } > Valor ingresado no valido.");
            }
        }

       

        public async Task SendMessagesToQueue(int numMessagesToSend)
        {
            var errorSend = 0;
            for (int i = 0; i < numMessagesToSend; i++)
            {
                try
                {
                    // Crear mensaje
                    var testServiceBusMessage = new TestMessage()
                    {
                        ExternalIdentifier = Guid.NewGuid(),
                        Identifier = i,
                        Name = $"Mensaje {i}"
                    };

                    // Create BrokeredMessage object
                    using (var brokeredMessage = new BrokeredMessage(testServiceBusMessage,
                                                                     new DataContractSerializer(typeof(TestMessage)))
                    {
                        Properties = {{ MessageType, typeof(TestMessage).FullName},
                                  { AssemblyName, typeof(TestMessage).AssemblyQualifiedName}}
                    })
                    {
                        Console.WriteLine($"{ DateTime.Now } > Enviando mensaje { i }");
                        // Enviando mensaje
                        await queueClient.SendAsync(brokeredMessage);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{ DateTime.Now } > Excepción: {exception.Message}");
                    errorSend++;
                }
            }

            await Task.Delay(10);

            Console.WriteLine($"{DateTime.Now} > { numMessagesToSend - errorSend } mensajes enviados.");
        }
    }
}
