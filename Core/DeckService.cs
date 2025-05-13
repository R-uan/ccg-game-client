using GameClient.Models;
using GameClient.Requests;
using GameClient.Settings;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using GameClient.Exceptions;

namespace GameClient.Core
{
    public class DeckService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthManager _authManager;
        private readonly ClientState _clientState;

        public DeckService(AuthManager authManager, ClientState clientState, AppSettings appSettings)
        {
            this._authManager = authManager;
            this._clientState = clientState;
            this._httpClient = new HttpClient() { BaseAddress = new Uri(appSettings.DeckServerAddr) };
        }

        public async Task FetchPlayerDeckCollectionAsync()
        {
            try
            {
                Logger.Info("Fetching player deck collection...");
                var response = await this._httpClient.GetAsync("/api/deck");
                if (!response.IsSuccessStatusCode)
                    throw new DeckRequestException($"Deck API returned {response.StatusCode}");
                var decks = await response.Content.ReadFromJsonAsync<List<Deck>>() ??
                            throw new ParsingResponseException("Unable to parse player deck response.");
                this._clientState.PlayerDecks = decks;
                Logger.Info("Successfully retrieved player's deck collection.");
            }
            catch (Exception e)
            {
                throw new DeckRequestException(e.Message);
            }
        }

        public async Task<Deck> PostPlayerDeckAsync(CreateDeckRequest request)
        {
            try
            {
                var response = await this._httpClient.PostAsJsonAsync("/api/deck", request);
                if (!response.IsSuccessStatusCode) throw new DeckRequestException("Deck creation request failed.");
                var deck = await response.Content.ReadFromJsonAsync<Deck>() ??
                           throw new ParsingResponseException("Unable to parse create deck response.");
                return deck;
            }
            catch (Exception e)
            {
                throw new DeckRequestException(e.Message);
            }
        }

        public void SetBearerToken()
        {
            this._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this._authManager.Token);
        }
    }
}