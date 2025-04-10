using System;
using System.Text.Json;
using GameClient.Requests;
using GameClient.Settings;

namespace GameClient.Core;

public class GameClient
{
    public AppSettings AppSettings { get; private set; }
    /// Services
    public AuthManager AuthManager { get; private set; }
    public DeckService DeckService { get; private set; }
    public CardService CardService { get; private set; }
    /// States
    public ClientState ClientState { get; private set; }

    public GameClient()
    {
        var configuration = JsonSerializer.Deserialize<AppSettings>(
            File.ReadAllText("settings.json")
        ) ?? throw new Exception("Could not get app settings.");

        this.AppSettings = configuration;
        this.ClientState = new ClientState();
        this.CardService = new CardService(this.ClientState, this.AppSettings);
        this.AuthManager = new AuthManager(this.ClientState, this.AppSettings);
        this.DeckService = new DeckService(this.AuthManager, this.ClientState, this.AppSettings);
    }

    public async Task<bool> Login(string email, string password)
    {
        var credentials = new LoginRequest(email, password);
        return await this.AuthManager.Login(credentials);
    }

    public async Task<bool> Register(string email, string username, string password)
    {
        var credentials = new RegisterRequest(email, username, password);
        return await this.AuthManager.Register(credentials);
    }

    public async Task Initialization()
    {
        await this.AuthManager.RequestPlayerData();
        await this.CardService.GetCardCollectionAsync();
        await this.DeckService.GetPlayerDeckCollectionAsync();
    }
}
