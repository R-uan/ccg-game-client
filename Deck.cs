using System;

namespace GameClient
{
    public class Deck
    {
        public string DeckId { get; set; }
        public string DeckName { get; set; }
        public List<string> Cards { get; set; }

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
