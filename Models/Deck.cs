namespace GameClient.Models
{
    public class Deck
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required List<CardReference> Cards { get; set; }

        public int MaxSize { get; set; }
        public int MinimumSize { get; set; }

        public bool AddCard(Guid cardId, int amount)
        {
            if ((Cards.Count + amount) > MaxSize) { return false; }
            var exists = this.Cards.Find(card => card.Id == cardId);
            if (exists != null)
            {
                exists.Amount += amount;
                return true;
            }

            var newCard = new CardReference(cardId, amount);
            this.Cards.Add(newCard);

            return true;
        }

        public bool RemoveCard(Guid cardId)
        {
            var card = this.Cards.Find(card => card.Id == cardId);
            if (card != null) return this.Cards.Remove(card);
            return false;
        }

    }
}
