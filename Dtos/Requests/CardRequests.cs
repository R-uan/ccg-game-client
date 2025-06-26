using GameClient.Models;

namespace GameClient.Requests
{
    public class SelectedCardsResponse
    {
        public required List<Card> Cards { get; set; }
        public required List<Guid> CardsNotFound { get; set; }
    }
}