using GameClient.Models;

namespace GameClient.Requests
{
    public class CreateDeckRequest
    {
        public required int DeckSize { get; set; }
        public required string Name { get; set; }
        public required string DeckType { get; set; }
        public required List<CardReference> Cards { get; set; }

        public CreateDeckRequest(string Name, string DeckType, List<CardReference> Cards)
        {
            this.DeckType = DeckType;
            this.Name = Name;
            this.Cards = Cards;

            var count = 0;
            Cards.ForEach(card => count += card.Amount);
            this.DeckSize = count;
        }
    }
}
