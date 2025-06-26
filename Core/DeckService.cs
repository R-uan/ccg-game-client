using System.Net;
using GameClient.Models;
using GameClient.Requests;
using GameClient.Settings;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using GameClient.Exceptions;

namespace GameClient.Core
{
    public class DeckService(AuthManager authManager, ClientState clientState, AppSettings appSettings)
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(appSettings.DeckServerAddr) };

        public async Task FetchPlayerDeckCollectionAsync()
        {
            Logger.Info("Fetching player deck collection...");
            var response = await this._httpClient.GetAsync("/api/deck");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var decks = await response.Content.ReadFromJsonAsync<List<Deck>>();
                if (decks == null) throw new ParsingResponseException("Unable to parse player deck response");
                Logger.Info("Deck Collection API: Successfully retrieved player's deck collection.");
                clientState.PlayerDecks = decks;
                return;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Deck Collection API: ({response.StatusCode}) -> {responseMessage}");
            throw new DeckRequestException("Unable to fetch player's deck collection");
        }

        public async Task<Deck> PostPlayerDeckAsync(CreateDeckRequest request)
        {
            var response = await this._httpClient.PostAsJsonAsync("/api/deck", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deck = await response.Content.ReadFromJsonAsync<Deck>();
                if (deck == null) throw new ParsingResponseException("Unable to parse create deck response");
                Logger.Info("Deck Collection API: Successfully created player's deck.");
                return deck;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Deck Collection API: ({response.StatusCode}) -> {responseMessage}");
            throw new DeckRequestException("Unable to create deck");
        }

        public void SetBearerToken()
            => this._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authManager.Token);
    }
}