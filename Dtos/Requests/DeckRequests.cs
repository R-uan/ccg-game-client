using GameClient.Models;

namespace GameClient.Requests
{
    public abstract class CreateDeckRequest
    {
        public required int DeckSize { get; set; }
        public required string Name { get; set; }
        public required string DeckType { get; set; }
        public required List<CardReference> Cards { get; set; }

        public CreateDeckRequest(string name, string deckType, List<CardReference> cards)
        {
            this.DeckType = deckType;
            this.Name = name;
            this.Cards = cards;

            var count = 0;
            cards.ForEach(card => count += card.Amount);
            this.DeckSize = count;
        }
    }
}
