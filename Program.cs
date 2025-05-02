using GameClient.Core;

static async Task run(string email, string password)
{
    GameClient.Core.GameClient client = new();
    var login = await client.Login(email, password);
    if (login.Success)
    {
        System.Console.WriteLine(client.AuthManager.Token);
        await client.Initialization();

        if (client.ClientState.PlayerDecks != null && client.ClientState.PlayerDecks.Count > 0)
        {
            if (client.ClientState.PlayerProfile != null && client.AuthManager.Token != null)
            {
                SynapseNet.start_connection("127.0.0.1", 8000);
                MatchService.ConnectPlayer(client.ClientState.PlayerProfile.Id, client.ClientState.PlayerDecks[1].Id, client.AuthManager.Token);
            }
        }
    }
    else
    {
        System.Console.WriteLine("Login fucked up");
    }

    Console.ReadKey(); // keeps process alive
}

await run("gameclient@test.com", "1234cinco");