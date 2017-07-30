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
        protected readonly static String ConnectionString = ConfigurationManager.AppSettings["SBConnectionString"].ToString();
        protected readonly static String QueueName = ConfigurationManager.AppSettings["QueueName"].ToString();

        CorreoController correo = new CorreoController();
        //Inicializando el cliente de colas
        QueueClient queueClient = QueueClient.CreateFromConnectionString(ConnectionString, QueueName);
        
        public int GET()
        {            
            Calculo calculo = null;
            Resultado resultado = null;
            try
            {
                // Obteniendo el mensaje
                queueClient.OnMessage(message => {

                    calculo = message.GetBody<Calculo>(); // Obteniendo el cuerpo del mensaje

                    resultado = Calcular(calculo); // Realizando la operación necesaria

                    Notificacion notificacion = new Notificacion(); // Construyendo la notificación
                    notificacion.Usuario = calculo.Usuario.Id;
                    notificacion.Titulo = $"Resultado de operación {resultado.operador}.";
                    notificacion.Subtitulo = $"{calculo.Numero1} {calculo.Operador} {calculo.Numero2}";
                    notificacion.Cuerpo = $"Su resultado es: {resultado.resultado}.";

                    var correoEnviado = correo.EnviarCorreo(calculo.Usuario.Correo, /* Enviando el correo */
                                    "Consulting Group Corporación Latinoaméricana",
                                    "Resultado de operación",
                                    "<hr>" +
                                    $"<h3>Sr./Sra. {calculo.Usuario.Apellido} el resultado de su operación es:</h3> <h2>{calculo.Numero1 + calculo.Numero2}</h2>" +
                                    "<hr>"
                                    ).GetAwaiter().GetResult();

                    if (correoEnviado)
                    {
                        notificacion.EnviarNotificacion(notificacion, calculo.Usuario); //Enviando la notificación
                    }
                    else
                    {
                        // proceso en caso de error al enviar correo
                    }

                }, new OnMessageOptions {
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
            public string operador { get; set; }
            public int resultado { get; set; }
        }

        private Resultado Calcular(Calculo calculo)
        {
            Resultado result;

            switch (calculo.Operador)
            {
                case (0):
                    result = new Resultado() { operador = "Suma", resultado = calculo.Numero1 + calculo.Numero2 };
                    break;
                case (1):
                    result = new Resultado() { operador = "Resta", resultado = calculo.Numero1 - calculo.Numero2 };
                    break;
                case (2):
                    result = new Resultado() { operador = "Multiplicación", resultado = calculo.Numero1 * calculo.Numero2 };
                    break;
                case (3):
                    result = new Resultado() { operador = "División", resultado = calculo.Numero1 / calculo.Numero2 };
                    break;
                default:
                    result = new Resultado() { operador = "Desconocido", resultado = -1 };
                    break;
            }

            return result;
        }
    }
}
