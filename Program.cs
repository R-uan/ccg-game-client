GameClient.Core.GameClient client = new();

if (await client.Login("gameclient@test.com", "1234cinco"))
{
    System.Console.WriteLine(client.AuthManager.Token);
}
else
{
    System.Console.WriteLine("Login fucked up");
}

Console.ReadKey(); // keeps process alive