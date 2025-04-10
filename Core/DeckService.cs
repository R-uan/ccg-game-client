using GameClient.Models;
using GameClient.Requests;
using GameClient.Settings;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace GameClient.Core
{
    public class DeckService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthManager _authManager;
        private readonly AppSettings _appSettings;
        private readonly ClientState _clientState;

        public DeckService(AuthManager authManager, ClientState clientState, AppSettings appSettings)
        {
            this._authManager = authManager;
            this._appSettings = appSettings;
            this._clientState = clientState;

            this._httpClient = new HttpClient()
            {
                BaseAddress = new Uri(this._appSettings.DeckServerAddr)
            };

            this._httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", this._authManager.Token);
        }

        public async Task GetPlayerDeckCollectionAsync()
        {
            try
            {
                var response = await this._httpClient.GetAsync("/api/deck");
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Deck API returned {response.StatusCode}");

                var decks = await response.Content.ReadFromJsonAsync<List<Deck>>()
                    ?? throw new Exception("Deck API returned no data.");
                this._clientState.PlayerDecks = decks;
            }
            catch (System.Exception)
            {
                // Need to make a logger
                throw new Exception("Failed to fetch player deck collection.");
            }
        }

        public async Task<Deck> PostNewPlayerDeckAsync(CreateDeckRequest request)
        {
            var response = await this._httpClient.PostAsJsonAsync("/api/deck", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Deck creation request failed.");
            var deck = await response.Content.ReadFromJsonAsync<Deck>() ?? throw new Exception("Didn't return sh..");
            return deck;
        }
    }
}
