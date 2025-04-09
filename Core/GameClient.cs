using System;
using System.Text.Json;
using GameClient.Requests;

namespace GameClient.Core;

public class GameClient
{
    public AuthManager Auth { get; private set; }
    public DeckService DeckService { get; private set; }
    public AppSettings AppSettings { get; private set; }
    public ClientState ClientState { get; private set; }

    public GameClient()
    {
        var configuration = JsonSerializer.Deserialize<AppSettings>(
            File.ReadAllText("settings.json")
        ) ?? throw new Exception("Could not get app settings.");

        this.AppSettings = configuration;
        this.ClientState = new ClientState();
        this.Auth = new AuthManager(this.AppSettings);
        this.DeckService = new DeckService(this.Auth, this.AppSettings);
    }

    public async Task Login(string email, string password)
    {
        var credentials = new LoginRequest(email, password);
        await this.Auth.Login(credentials);
    }

    public async Task RequestPlayerData()
    {
        this.ClientState.PlayerDecks = await this.DeckService.GetPlayerDeckCollectionAsync();
        await this.Auth.RequestPlayerData();
    }
}
