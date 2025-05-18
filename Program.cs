using GameClient.Core;
namespace GameClient
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Hello World!");
            var game = new Core.GameClient();
            await game.MatchService.ConnectToMatch();
            
            Console.ReadKey();
        }
    }
}

