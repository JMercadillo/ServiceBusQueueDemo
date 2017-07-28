using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Espacios de nombres agregados
using ServiceBusMessagingDemo;

namespace ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"{ DateTime.Now } > Creando QueueClient.");
                Microsoft.ServiceBus.Messaging.QueueClient queueClient = Log.CreateLog();
                Console.WriteLine($"{ DateTime.Now } > QueueClient creado correctamente.");
                for (int i = 0; i < 60; i++) Console.Write("-");

                Console.WriteLine($"\n{DateTime.Now} > Bienvenido, seleccione una opción:");
                for (int i = 0; i < 60; i++) Console.Write("-");

                Console.WriteLine($"\n{ DateTime.Now } > Enviar mensajes\t=> 1");
                Console.WriteLine($"{ DateTime.Now } > Recibir mensajes\t=> 2");
                Console.WriteLine($"{ DateTime.Now } > Salir\t\t=> 3");
                for (int i = 0; i < 60; i++) Console.Write("-");
                Console.Write($"\n{DateTime.Now} > ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case ("1"): // enviar mensajes
                        new SendMessage().Main(queueClient).GetAwaiter().GetResult();
                        break;
                    case ("2"): // recibir mensajes
                        new ReceiveMessage().Main(queueClient).GetAwaiter().GetResult();
                        break;
                    case ("3"): // saliendo
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine($"{ DateTime.Now } > Opción no valida.");
                        break;
                }

                                
            }
            catch (Exception excepcion)
            {
                Console.WriteLine($"{ DateTime.Now } > Execpción: { excepcion.Message }");
            }
            finally
            {
                Console.WriteLine($"{ DateTime.Now } > Presione una tecla para salir.");
                Console.ReadKey();
            }
        }
    }
}
