using GameClient.Models;
using GameClient.Settings;
using System.Net.Http.Json;

namespace GameClient.Core
{
    public class CardService
    {
        private HttpClient _httpClient;
        private readonly ClientState _clientState;
        public List<Card>? CardCollection { get; private set; }

        public CardService(ClientState clientState, AppSettings appSettings)
        {
            this._clientState = clientState;
            this._httpClient = new HttpClient
            {
                BaseAddress = new Uri(appSettings.CardServerAddr)
            };
        }

        public async Task GetCardCatalogAsync()
        {
            Logger.Info("Fetching card catalog...");
            var request = await this._httpClient.GetAsync("/api/cards/catalog");
            if (!request.IsSuccessStatusCode) throw new Exception("Unable to get card collection");
            var cardCollection = await request.Content.ReadFromJsonAsync<List<Card>>()
                ?? throw new Exception("Card API didnt return anything");
            this._clientState.CardCollection = cardCollection;
            Logger.Info($"Found {cardCollection.Count} cards total.");
        }

        public async Task<SelectedCardsResponse> GetSelectedCardsAsync(List<Guid> cardIds)
        {
            try
            {
                Logger.Info($"Fetching {cardIds.Count} selected cards...");
                var request = await this._httpClient.PostAsJsonAsync("/api/cards/deck", cardIds);
                if (!request.IsSuccessStatusCode) throw new Exception("Unable to get card collection");
                var selectedCards = await request.Content.ReadFromJsonAsync<SelectedCardsResponse>()
                    ?? throw new Exception("Card API didnt return anything");
                Logger.Info($"Fetched {selectedCards.Cards.Count} cards sucessfully.");
                Logger.Info($"{selectedCards.CardsNotFound.Count} cards' were not found.");
                Logger.Info($"{selectedCards.InvalidCardGuid.Count} cards' IDs were not valid GUIDs.");
                return selectedCards;

            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
