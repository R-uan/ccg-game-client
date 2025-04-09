using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GameClient.Core
{
    public class DeckService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthManager _authManager;
        private readonly AppSettings _appSettings;


        public DeckService(AuthManager authManager, AppSettings appSettings)
        {
            this._authManager = authManager;
            this._appSettings = appSettings;

            this._httpClient = new HttpClient()
            {
                BaseAddress = new Uri(this._appSettings.DeckServerAddr)
            };

            this._httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", this._authManager.Token.ToString());
        }

        public async Task SaveDeckAsync(Deck deck)
        {

        }

        public async Task<List<Deck>> GetPlayerDeckCollectionAsync()
        {
            try
            {
                var response = await this._httpClient.GetAsync("/api/decks");
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Deck API returned {response.StatusCode}");

                var decks = await response.Content.ReadFromJsonAsync<List<Deck>>();
                return decks ?? throw new Exception("Deck API returned no data.");

            }
            catch (System.Exception)
            {
                // Need to make a logger
                throw new Exception("Failed to fetch player deck collection.");
            }
        }
    }
}
