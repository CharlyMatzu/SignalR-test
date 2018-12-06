using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class PrintHub : Hub
    {

        // array de conexiones
        //public static HashSet<string> ConnectedIds = new HashSet<string>();
        public static List<string> ConnectedIds = new List<string>();

        // Nuevas conexiones
        public override Task OnConnectedAsync()
        {
            //Console.WriteLine("FUNCIONA");
            // Almacena en array
            PrintHub.ConnectedIds.Add( Context.ConnectionId );
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        // Envio de notificaciones
        public async Task SendNotify(string message)
        {
            // Envia a todos usando el evento "Notification"
            //await Clients.All.SendAsync("Notification", url);

            // Envia su ID a cada cliente conectado usando el evento "Notification"
            foreach (String id in PrintHub.ConnectedIds)
            {
                //await Clients.All.SendAsync("Notification", "El ID es: " + id);
                await Clients.Client(id).SendAsync("Notification", "Tu ID es: " + id);
            }

        }


    }
}

