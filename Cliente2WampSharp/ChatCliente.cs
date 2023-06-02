using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2.Client;
using WampSharp.V2.Fluent;
using WampSharp.V2;
using WampSharp.V2.Rpc;

namespace Cliente2WampSharp
{
    public class ChatCliente
    {
        public async Task ConnectToChat()
        {
            const string location = "ws://127.0.0.1:8080/ws";

            // Se crea un canal de comunicacion con el servidor
            DefaultWampChannelFactory channelFactory = new DefaultWampChannelFactory();
            IWampChannel channel = channelFactory.CreateJsonChannel(location, "realm1");

            // Se abre canal para establecer la conexion con el servidor
            Task openTask = channel.Open();
            await openTask.ConfigureAwait(false);

            // Se crea un proxy del realm para interactuar con los servicios
            IWampRealmProxy realmProxy = channel.RealmProxy;

            //
            IChatRPC instance = realmProxy.Services.GetCalleeProxy<IChatRPC>();

            // Se solicita al usuario un nombre de usuario
            string username = GetusernameFromUser();

            //
            await instance.SendMessage($"{username} Se ha unido al chat");

            Console.WriteLine($" {username} Conexion establecida. ¡Comienza a enviar mensajes! o escriba 'exit' para salir");

            bool exit = false;
            while (!exit)
            {
                // Lee el mensaje ingresado por el usuario
                string message = Console.ReadLine();
                if (message.ToLower() == "exit")
                {
                    
                    await instance.SendMessage($"{username} Se ha desconetado");
                    exit = true;
                }
                else
                {
                    
                    await instance.SendMessage($"{username}: {message}");
                }
            }

        }

        private string GetusernameFromUser()
        {
            Console.WriteLine("Ingresa tu nombre de usuario:");
            return Console.ReadLine();
        }


        public interface IChatRPC
        {
            [WampProcedure("com.chat.sendMessage")]
            Task SendMessage(string message);
        }
    }
}
