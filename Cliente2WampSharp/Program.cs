
namespace Cliente2WampSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Iniciando chat 2...");

            ChatCliente server = new ChatCliente();
            await server.ConnectToChat();

        }
    }
}