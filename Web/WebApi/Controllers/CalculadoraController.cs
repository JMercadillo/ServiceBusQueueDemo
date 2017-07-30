using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CalculadoraController : ApiController
    {
        //Obteniendo credenciales y nombre de la cola
        String ConnectionString = ConfigurationManager.AppSettings["SBConnectionString"].ToString();
        String QueueName = ConfigurationManager.AppSettings["QueueName"].ToString();

        public int GET()
        {            
            Calculo calculo = null;
            Resultado resultado = null;

            //Inicializando el cliente de colas
            QueueClient queueClient = QueueClient.CreateFromConnectionString(ConnectionString, QueueName);

            try
            {
                //Obteniendo el mensaje
                queueClient.OnMessage(message =>
                {
                    //var message = queueClient.Receive();

                    if (message != null)
                    {
                        calculo = message.GetBody<Calculo>(); // Obteniendo el cuerpo del mensaje

                        resultado = Calcular(calculo); // Realizando la operación necesaria

                        Notificacion notificacion = new Notificacion(); // Construyendo la notificación
                        notificacion.Usuario = calculo.Usuario.Id;
                        notificacion.Titulo = $"Resultado de operación {resultado.nombreOperador}.";
                        notificacion.Subtitulo = $"{calculo.Numero1} {resultado.operador} {calculo.Numero2} = ";
                        notificacion.Cuerpo = $"Su resultado es: <b>{resultado.resultado}</b>.";

                        notificacion.EnviarNotificacion(notificacion, calculo.Usuario); //Enviando la notificación

                        CorreoController correo = new CorreoController();
                        correo.EnviarCorreo(calculo.Usuario.Correo, /* Enviando el correo */
                                        "Consulting Group Corporación Latinoaméricana",
                                        "Resultado de operación",
                                        "<hr>" +
                                        $"<h3>{calculo.Usuario.Apellido} el resultado de su operación ({resultado.nombreOperador}) es:</h3> <h2>{calculo.Numero1} {resultado.operador} {calculo.Numero2} = <b>{resultado.resultado}</b></h2>" +
                                        "<hr>"
                                        ).GetAwaiter().GetResult();

                        message.Complete();
                    }
                    else
                    {
                        queueClient.Close();
                    }
                }, new OnMessageOptions
                {
                    AutoComplete = true,
                    MaxConcurrentCalls = 1
                });
            }
            catch (ObjectDisposedException ex) // Excepcion cuando no se pueda deseliarizar el mensaje
            {
                return -1;
            }
            catch (Exception ex) // Excepción cuando se desconozca 
            {
                return 0;
            }
            
            return 1;
        }

        private class Resultado
        {
            public string nombreOperador { get; set; }
            public string operador { get; set; }
            public int resultado { get; set; }
        }

        private Resultado Calcular(Calculo calculo)
        {
            Resultado result;

            switch (calculo.Operador)
            {
                case (0):
                    result = new Resultado() { nombreOperador = "Suma", operador = "+", resultado = calculo.Numero1 + calculo.Numero2 };
                    break;
                case (1):
                    result = new Resultado() { nombreOperador = "Resta", operador = "-", resultado = calculo.Numero1 - calculo.Numero2 };
                    break;
                case (2):
                    result = new Resultado() { nombreOperador = "Multiplicación", operador = "x", resultado = calculo.Numero1 * calculo.Numero2 };
                    break;
                case (3):
                    result = new Resultado() { nombreOperador = "División", operador = "/", resultado = calculo.Numero1 / calculo.Numero2 };
                    break;
                default:
                    result = new Resultado() { nombreOperador = "Desconocido", operador = "¿?", resultado = -1 };
                    break;
            }

            return result;
        }
    }
}
