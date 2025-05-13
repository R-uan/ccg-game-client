using GameClient.Models;
using GameClient.Settings;
using System.Net.Http.Json;
using GameClient.Exceptions;

namespace GameClient.Core
{
    public class CardService
    {
        private readonly HttpClient _httpClient;
        private readonly ClientState _clientState;
        public List<Card>? CardCollection { get; private set; }

        public CardService(ClientState clientState, AppSettings appSettings)
        {
            this._clientState = clientState;
            this._httpClient = new HttpClient { BaseAddress = new Uri(appSettings.CardServerAddr) };
        }

        public async Task FetchCardCatalogAsync()
        {
            Logger.Info("Fetching card catalog...");
            var request = await this._httpClient.GetAsync("/api/cards/catalog");
            if (!request.IsSuccessStatusCode) throw new CardRequestException("Unable to request card collection.");
            var cardCollection = await request.Content.ReadFromJsonAsync<List<Card>>() ??
                                 throw new ParsingResponseException("Unable to parse card collection.");
            this._clientState.CardCollection = cardCollection;
            Logger.Info($"Found {cardCollection.Count} cards total.");
        }

        public async Task<SelectedCardsResponse> FetchSelectedCardsAsync(List<Guid> cardIds)
        {
            Logger.Info($"Fetching {cardIds.Count} selected cards...");
            var request = await this._httpClient.PostAsJsonAsync("/api/cards/deck", cardIds);
            if (!request.IsSuccessStatusCode)
                throw new CardRequestException("Unable to get selected cards collection.");
            var selectedCards = await request.Content.ReadFromJsonAsync<SelectedCardsResponse>() ??
                                throw new ParsingResponseException("Card API didnt return anything");
            Logger.Info($"Fetched {selectedCards.Cards.Count} cards successfully.");
            Logger.Info($"{selectedCards.CardsNotFound.Count} cards' were not found.");
            Logger.Info($"{selectedCards.InvalidCardGuid.Count} cards' IDs were not valid GUIDs.");
            return selectedCards;
        }
    }
}