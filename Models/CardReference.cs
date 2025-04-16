namespace GameClient.Models
{
    public class CardReference
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }

        public CardReference() { }
        public CardReference(Guid cardId, int amount) { this.Id = cardId; this.Amount = amount; }
    }
}