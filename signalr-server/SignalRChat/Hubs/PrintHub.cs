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


        /// <summary>
        /// Se ejecuta cuando un cliente se conecta del servidor y almacena dicho id
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            // se obtiene id desconectado
            var id = Context.ConnectionId;
            // Almacena en array
            PrintHub.ConnectedIds.Add( id );
            // Mensaje para servidor
            Clients.All.SendAsync("Connect", "Se conecto el cliente " + id);
            return base.OnConnectedAsync();
        }



        /// <summary>
        /// Se ejecuta cuando un cliente se desconecta del servidor y remueve id de array
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            // se obtiene id desconectado
            var id = Context.ConnectionId;
            // se remueve Id de array
            PrintHub.ConnectedIds.Remove( Context.ConnectionId );

            // Mensaje para servidor
            Clients.All.SendAsync("Disconnect", "Se desconecto el cliente " + id);

            return base.OnDisconnectedAsync(exception);
        }



        /// <summary>
        /// Envio de notificaciones ejecutado por 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendNotify(string message)
        {
            // Envia a todos usando el evento "Notification"
            await Clients.All.SendAsync("Notification", message);
        }

        

        /// <summary>
        /// Envia a todos los clientes a su propio id
        /// </summary>
        /// <returns></returns>
        public async Task SendIds()
        {
            // Envia su ID a cada cliente conectado usando el evento "Notification"
            foreach (String id in PrintHub.ConnectedIds)
            {
                //await Clients.All.SendAsync("Notification", "El ID es: " + id);
                await Clients.Client(id).SendAsync("Notification", "Tu ID es: " + id);
            }
        }


    }
}

