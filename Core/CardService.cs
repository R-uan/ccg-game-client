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
            this._httpClient = new HttpClient();
            this._httpClient.BaseAddress = new Uri(appSettings.CardServerAddr);
        }

        public async Task GetCardCollectionAsync()
        {
            var request = await this._httpClient.GetAsync("/api/cards");
            if (!request.IsSuccessStatusCode) throw new Exception("Unable to get card collection");
            var cardCollection = await request.Content.ReadFromJsonAsync<List<Card>>()
                ?? throw new Exception("Card API didnt return anything");
            this._clientState.CardCollection = cardCollection;
        }
    }
}
