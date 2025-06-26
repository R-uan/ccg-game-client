using System.Net;
using GameClient.Models;
using GameClient.Settings;
using System.Net.Http.Json;
using GameClient.Exceptions;
using GameClient.Requests;

namespace GameClient.Core
{
    public class CardService(ClientState clientState, AppSettings appSettings)
    {
        public List<Card>? CardCollection { get; private set; }
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(appSettings.CardServerAddr) };

        public async Task FetchCardCatalogAsync()
        {
            Logger.Info($"Fetching all cards from Card Collection API.");
            var response = await this._httpClient.GetAsync("/api/cards");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var cardCollection = await response.Content.ReadFromJsonAsync<List<Card>>();
                if (cardCollection == null) throw new ParsingResponseException("Unable to parse card collection.");
                Logger.Info($"Card Collection API has returned a total of: `{cardCollection.Count}` cards.");
                clientState.CardCollection = cardCollection;
                return;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Card Collection API [{response.StatusCode}]: {responseMessage}");
            throw new CardRequestException("Unable to get card collection");
        }

        public async Task<SelectedCardsResponse> FetchSelectedCardsAsync(List<Guid> cardIds)
        {
            Logger.Info($"Fetching {cardIds.Count} cards from Card Collection API.");
            var response = await this._httpClient.PostAsJsonAsync("/api/cards/deck", cardIds);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var selectedCards = await response.Content.ReadFromJsonAsync<SelectedCardsResponse>();
                if (selectedCards == null) throw new ParsingResponseException("Card API didnt return anything");
                Logger.Info($"Card Collection API returned {selectedCards.Cards.Count}/{cardIds.Count} cards.");
                return selectedCards;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            Logger.Error($"Card Collection API: ({response.StatusCode}) -> {responseMessage}");
            throw new CardRequestException("Unable to get selected card collection");
        }
    }
}