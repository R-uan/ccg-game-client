using System.Net.Http.Headers;
using System.Net.Http.Json;
using GameClient.Requests;

namespace GameClient.Core
{
    public class AuthManager
    {
        private readonly HttpClient _httpClient;
        public Guid? Token { get; private set; }
        public PlayerProfile? PlayerProfile { get; private set; }

        public AuthManager(AppSettings appSettings)
        {
            this._httpClient = new HttpClient();
            this._httpClient.BaseAddress = new Uri(appSettings.AuthServerAddr);
        }

        public async Task<bool> Login(LoginRequest credentials)
        {
            try
            {
                var request = await this._httpClient.PostAsJsonAsync("/api/auth/login", credentials);
                if (!request.IsSuccessStatusCode)
                    throw new Exception(await request.Content.ReadAsStringAsync());
                var response = await request.Content.ReadFromJsonAsync<LoginRespose>();
                if (response != null && Guid.TryParse(response.Token, out var token))
                {
                    this.Token = token;
                    return true;
                }

                return false;
            }
            catch (System.Exception)
            {
                throw new Exception("Unable to login.");
            }
        }

        public async Task<bool> Register(RegisterRequest credentials)
        {
            try
            {
                var request = await this._httpClient.PostAsJsonAsync("/api/auth/register", credentials);
                if (!request.IsSuccessStatusCode)
                    throw new Exception(await request.Content.ReadAsStringAsync());
                var response = await request.Content.ReadFromJsonAsync<RegisterRespose>();
                return true;
            }
            catch (System.Exception)
            {
                throw new Exception("Unable to register player.");
            }
        }

        public async Task<bool> RequestPlayerData()
        {
            if (!this.IsLoggedin())
                throw new Exception("Client not logged in");

            this._httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", this.Token.ToString());

            var request = await this._httpClient.GetAsync("/api/auth/profile");
            var profile = await request.Content.ReadFromJsonAsync<PlayerProfile>() ??
                throw new Exception("Player API didn't send back data.");

            this.PlayerProfile = profile;
            return true;
        }

        public bool IsLoggedin() => this.Token != null;
    }
}