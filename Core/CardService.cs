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

        public async Task GetCardCollectionAsync()
        {
            System.Console.WriteLine("Requesting card collection");
            var request = await this._httpClient.GetAsync("/api/cards/catalog");
            if (!request.IsSuccessStatusCode) throw new Exception("Unable to get card collection");
            var cardCollection = await request.Content.ReadFromJsonAsync<List<Card>>()
                ?? throw new Exception("Card API didnt return anything");
            this._clientState.CardCollection = cardCollection;
            System.Console.WriteLine("card collection successfuly gotten");
        }

        public async Task<SelectedCardsResponse> GetSelectedCardsAsync(List<Guid> cardIds)
        {
            var request = await this._httpClient.GetAsync("/api/cards/deck");
            if (!request.IsSuccessStatusCode) throw new Exception("Unable to get card collection");
            var selectedCards = await request.Content.ReadFromJsonAsync<SelectedCardsResponse>()
                ?? throw new Exception("Card API didnt return anything");
            return selectedCards;
        }
    }
}
