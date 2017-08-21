using Microsoft.AspNet.SignalR;
using Seguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Notifications
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
                if (connectedClients.ContainsKey(usuario.UsuarioId))
                {
                    var client = connectedClients[usuario.UsuarioId];
                    client.notify("added"); // Indicandole al usuario que tiene una notificación
                }
            }
        }
    }
}
