using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;
using WebApp.Notifications;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApp.Notifications
{
    public class NotificationHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        private static Dictionary<int, dynamic> connectedClients = new Dictionary<int, dynamic>();

        
        public void RegisterClient(int UserId)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(UserId))
                {
                    connectedClients[UserId] = Clients.Caller;
                }
                else
                {
                    connectedClients.Add(UserId, Clients.Caller);
                }
            }
        }

        public void AddNotification(Usuario usuario)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(usuario.Id))
                {
                    var client = connectedClients[usuario.Id]; 
                    client.notify("added"); // Indicandole al usuario que tiene una notificación
                }
            }
        }
    }
}