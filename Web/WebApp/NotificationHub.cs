using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;
using WebApp.Notifications;

namespace WebApp
{
    public class NotificationHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        private static Dictionary<Usuario, dynamic> connectedClients = new Dictionary<Usuario, dynamic>();

        public void RegisterClient(Usuario User)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(User))
                {
                    connectedClients[User] = Clients.Caller();
                }
                else
                {
                    connectedClients.Add(User, Clients.Caller);
                }
            }
        }

        public void AddNotification(Usuario usuario)
        {
            lock (connectedClients)
            {
                if (connectedClients.ContainsKey(usuario))
                {
                    dynamic client = connectedClients[usuario];
                    client.notify("added"); // Indicandole al usuario que tiene una notificación
                }
            }
        }
    }
}