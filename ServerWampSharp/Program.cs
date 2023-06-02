
namespace ServerWampSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Iniciando Servidor espere un momento...");

            ChatServer server = new ChatServer();
            await server.RunServer();
            
        }
    }
}