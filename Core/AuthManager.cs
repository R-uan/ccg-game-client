using System.Net.Http.Headers;
using System.Net.Http.Json;
using GameClient.Models;
using GameClient.Requests;
using GameClient.Settings;

namespace GameClient.Core
{
    public class AuthManager
    {
        public event Action? OnAuthentication;
        private readonly HttpClient _httpClient;
        private readonly ClientState _clientState;
        public string? Token { get; private set; }

        public AuthManager(ClientState clientState, AppSettings appSettings)
        {
            this._clientState = clientState;
            this._httpClient = new HttpClient
            {
                BaseAddress = new Uri(appSettings.AuthServerAddr)
            };
        }

        public async Task Login(LoginRequest credentials)
        {
            try
            {
                var request = await this._httpClient.PostAsJsonAsync("/api/auth/login", credentials);
                if (!request.IsSuccessStatusCode) throw new Exception(await request.Content.ReadAsStringAsync());
                var response = await request.Content.ReadFromJsonAsync<LoginRespose>()
                    ?? throw new Exception("Auth API returned nothing.");

                this.Token = response.Token;
                this.OnAuthentication?.Invoke();
            }
            catch (System.Exception)
            {
                throw new Exception("Unable to login.");
            }
        }

        public async Task Register(RegisterRequest credentials)
        {
            try
            {
                var request = await this._httpClient.PostAsJsonAsync("/api/auth/register", credentials);
                if (!request.IsSuccessStatusCode) throw new Exception(await request.Content.ReadAsStringAsync());
                var response = await request.Content.ReadFromJsonAsync<RegisterRespose>()
                    ?? throw new Exception("Auth API returned nothing.");
            }
            catch (System.Exception)
            {
                throw new Exception("Unable to register player.");
            }
        }

        public async Task RequestPlayerProfile()
        {
            System.Console.WriteLine("Requesting player profile");
            if (!this.IsLoggedin())
                throw new Exception("Client not logged in");


            var request = await this._httpClient.GetAsync("/api/player/profile");
            var profile = await request.Content.ReadFromJsonAsync<PlayerProfile>() ??
                throw new Exception("Player API didn't send back data.");

            this._clientState.PlayerProfile = profile;
            System.Console.WriteLine("Player profile sucessfuly requested");
        }

        public void SetBearerToken()
        {
            this._httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", this.Token);
        }

        public bool IsLoggedin() => this.Token != null;
    }
}