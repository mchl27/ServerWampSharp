using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WampSharp.V2.Realm;
using WampSharp.V2;
using WampSharp.V2.Rpc;

namespace ServerWampSharp
{
    public class ChatServer
    {
        public async Task RunServer()
        {
            const string location = "ws://127.0.0.1:8080/ws";
            using (IWampHost host = new DefaultWampHost(location))
            {
                // Obten el realm "realm1" del host
                IWampHostedRealm realm = host.RealmContainer.GetRealmByName("realm1");

                              
                IChatRPC instance = new ChatRPC();

                Task<IAsyncDisposable> registrationTask = realm.Services.RegisterCallee(instance);

                await registrationTask;
                               
                // Abre host para aceptar las conexiones
                host.Open();

                Console.WriteLine("El servidor de chat esta en ejecucion en la ruta: " + location);
                Console.ReadLine();
            }
        }
    }

    public interface IChatRPC
    {
        [WampProcedure("com.chat.sendMessage")]
        void SendMessage(string message);
    }

    public class ChatRPC : IChatRPC
    {
        // Metodo RPC que ayuda al manejo de envio de mensajes
        public void SendMessage(string message) 
        {
            Console.WriteLine($"{DateTime.Now}: " +message);
        }
    }

}
