﻿using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.Helper
{
    public class CalculadoraHelper
    {
        string Uri = "http://localhost:57607/api/Calculadora";
        
        public int Get()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);
                HttpResponseMessage response = client.GetAsync(Uri).Result;
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

        /*
         * Enviando mensajes a la cola
         */
        public int EnviarOperar(Calculo obj)
        {
            try
            {
                var ConnectionString = ConfigurationManager.AppSettings["SBConnectionString"].ToString();
                var QueueName = ConfigurationManager.AppSettings["QueueName"].ToString();
                QueueClient queueClient = QueueClient.CreateFromConnectionString(ConnectionString, QueueName);

                BrokeredMessage message = new BrokeredMessage(obj);
                queueClient.Send(message);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}