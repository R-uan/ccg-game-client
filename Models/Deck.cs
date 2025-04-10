namespace GameClient.Models
{
    public class Deck
    {
        public Guid DeckId { get; set; }
        public required string DeckName { get; set; }
        public required List<string> Cards { get; set; }

        public int MaxSize { get; set; }
        public int MinimumSize { get; set; }

        public bool AddCard(string cardId)
        {
            if (Cards.Count >= MaxSize) { return false; }
            Cards.Add(cardId);
            return true;
        }

        public bool RemoveCard(string cardId) => Cards.Remove(cardId);

    }
}
