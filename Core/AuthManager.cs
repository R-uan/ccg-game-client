using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GameClient.Exceptions;
using GameClient.Models;
using GameClient.Requests;
using GameClient.Settings;

namespace GameClient.Core
{
    public class AuthManager(ClientState clientState, AppSettings appSettings)
    {
        public event Action? OnAuthentication;
        public string? Token { get; private set; }
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(appSettings.AuthServerAddr) };

        public async Task Login(LoginRequest credentials)
        {
            Logger.Info("Attempting to log in...");
            var response = await this._httpClient.PostAsJsonAsync("/api/auth/login", credentials);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var logged = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (logged == null) throw new ParsingResponseException("Unable to parse login response.");
                Logger.Debug("Player Auth API: Player logged in successfully.");
                this.Token = logged.Token;
                this.OnAuthentication?.Invoke();
                return;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Card Collection API [{response.StatusCode}]: {responseMessage}");
            throw new FailedLoginRequestException("Failed to Log In");
        }

        public async Task Register(RegisterRequest credentials)
        {
            Logger.Info("Requesting player registration from the Player Auth API.");
            var response = await this._httpClient.PostAsJsonAsync("/api/auth/register", credentials);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var registered = await response.Content.ReadFromJsonAsync<RegisterResponse>();
                if (registered == null) throw new ParsingResponseException("Unable to parse register response.");
                Logger.Info("Player Auth API: Player successfully registered.");
                this.Token = registered.Token;
                this.OnAuthentication?.Invoke();
                return;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Card Collection API [{response.StatusCode}]: {responseMessage}");
            throw new FailedRegistrationRequestException("Failed to Register");
        }

        public async Task FetchPlayerAccountData()
        {
            Logger.Info("Requesting player's data from the Player Auth API.");
            var response = await this._httpClient.GetAsync("/api/player/account");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var account = await response.Content.ReadFromJsonAsync<PlayerAccount>();
                if (account == null)
                    throw new ParsingResponseException("Player Auth API: Failed to parse player profile.");
                Logger.Info("Player Auth API: Successfully retrieved player's profile.");
                clientState.PlayerAccount = account;
                return;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Player Auth API: ({response.StatusCode}) -> {responseMessage}");
        }

        public void SetBearerToken()
            => this._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this.Token);


        public bool IsLoggedIn() => this.Token != null;
    }
}