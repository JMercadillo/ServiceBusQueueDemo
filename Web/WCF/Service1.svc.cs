using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //Funciona como helper o puede tener la operación
        public int Resultado(Calculo calculo)
        {
            string Uri = "http://localhost:57607/api/Calculadora";
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(Uri + $"?Numero1={calculo.Numero1}&Operador={calculo.Operador}&Numero2={calculo.Numero2}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<int>().Result;
                    return result;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task GetMessage()
        {
            string resultado = "-1";
            var calculo = new Calculo();
            BrokeredMessage message = null;

            //Obteniendo credenciales y nombre de la cola
            var ConnectionString = ConfigurationManager.AppSettings["SBConnectionString"].ToString();
            var QueueName = ConfigurationManager.AppSettings["QueueName"].ToString();
            //Inicializando el cliente de colas
            QueueClient queueClient = QueueClient.CreateFromConnectionString(ConnectionString, QueueName);

            do
            {
                try
                {
                    message = await queueClient.ReceiveAsync(); // Obteniendo el mensaje
                }
                catch (ObjectDisposedException ex)
                {
                    resultado = $"Ocurrió un problema al procesar su petición: {ex.Message}";
                    message = null;
                }
                if (message != null)
                {
                    try
                    {
                        calculo = message.GetBody<Calculo>();// Obteniendo el cuerpo del mensaje

                        resultado = Resultado(calculo).ToString();// Procesando el mensaje

                        await message.CompleteAsync();// Eliminando el mensaje procesado de la cola
                    }
                    catch (Exception ex)
                    {
                        resultado = $"Ocurrió un problema al deserializar su mensaje: {ex.Message}";
                    }
                    finally
                    {
                        if (!resultado.Contains("problema"))
                        {
                            await sendEmail(calculo.Usuario.Correo,
                                    "Consulting Group Corporación Latinoaméricana",
                                    "Resultado de operación",
                                    "<hr>" +
                                    $"<h3>Sr./Sra. {calculo.Usuario.Apellido} el resultado de su operación es:</h3> <h2>{resultado}</h2>" +
                                    "<hr>"
                                    );
                        }
                        else
                        {
                            await sendEmail(calculo.Usuario.Correo,
                                    "Consulting Group Corporación Latinoaméricana",
                                    "Resultado de operación",
                                    "<hr>" +
                                    $"<h3>Sr./Sra. {calculo.Usuario.Apellido} su operación no se pudo realizar.</h3>" +
                                    "<hr>"
                                    );
                        }
                    }
                }
                else
                {
                    break;
                }

                await Task.Delay(2);

            } while (message != null);

            queueClient.Close();
        }

        private async Task sendEmail(string EmailTo, string NameFrom, string Subject, string Body)
        {
            var EmailFrom = ConfigurationManager.AppSettings["Email"].ToString();
            var Password = ConfigurationManager.AppSettings["Pass"].ToString();

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(EmailTo);
            msg.From = new MailAddress(EmailFrom, NameFrom, System.Text.Encoding.UTF8);
            msg.Subject = Subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = Body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(EmailFrom, Password);
            client.Port = 587;
            client.Host = "smtp.office365.com";
            client.EnableSsl = true;
            try
            {
                await client.SendMailAsync(msg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
