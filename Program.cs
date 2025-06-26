using GameClient.Core;
namespace GameClient
{
    public static class Program
    {
        public static async Task Main()
        {
            var game = new Core.GameClient();
            var login = await game.Login("gameclient@test.com", "1234cinco");
            if (login.Success)
            {
                await game.Initialization();
                Thread.Sleep(2000);
                await game.MatchService.PlayCard();
            }
            else
            {
                Logger.Error("Failed to login");
                Logger.Error(login.Error);
            }
            Console.ReadKey();
        }
    }
}

