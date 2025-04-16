GameClient.Core.GameClient client = new();

var login = await client.Login("gameclient@test.com", "1234cinco");
if (login.Success)
{
    System.Console.WriteLine(client.AuthManager.Token);
    await client.Initialization();
}
else
{
    System.Console.WriteLine("Login fucked up");
}

Console.ReadKey(); // keeps process alive