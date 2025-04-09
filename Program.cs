// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using GameClient.Core;

[DllImport("libGameClient")]
static extern void start_connection(string addr, int port);

[DllImport("libGameClient")]
static extern void connect_player();

GameClient.Core.GameClient client = new();

Console.ReadKey(); // keeps process alive