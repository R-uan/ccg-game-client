using System;
using System.Text.Json;
using GameClient.Requests;
using GameClient.Settings;
using GameClient.Validators;

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

        AuthManager.OnAuthentication += this.DeckService.SetBearerToken;
        AuthManager.OnAuthentication += this.AuthManager.SetBearerToken;
    }

    public async Task<Result<bool>> Login(string email, string password)
    {
        try
        {
            var loginValidator = new LoginRequestValidator();
            var credentials = new LoginRequest(email, password);
            var validateRequest = loginValidator.Validate(credentials);
            if (!validateRequest.IsValid) return Result<bool>.Fail(validateRequest.Errors.First().ErrorMessage);
            await this.AuthManager.Login(credentials);
            return Result<bool>.Ok(true);
        }
        catch (System.Exception ex)
        {
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> Register(string email, string username, string password)
    {
        try
        {
            var registerValidator = new RegisterRequestValidator();
            var credentials = new RegisterRequest(email, username, password);
            var validateRequest = registerValidator.Validate(credentials);
            if (!validateRequest.IsValid) return Result<bool>.Fail(validateRequest.Errors.First().ErrorMessage);
            await this.AuthManager.Register(credentials);
            return Result<bool>.Ok(true);
        }
        catch (System.Exception ex)
        {
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task Initialization()
    {
        await this.AuthManager.RequestPlayerProfile();
        await this.CardService.GetCardCollectionAsync();
        await this.DeckService.GetPlayerDeckCollectionAsync();
    }
}
