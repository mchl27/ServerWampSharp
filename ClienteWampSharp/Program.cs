
namespace ClienteWampSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Iniciando chat 1...");

            ChatCliente server = new ChatCliente();
            await server.ConnectToChat();

        }
    }
}