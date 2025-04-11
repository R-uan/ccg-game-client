namespace GameClient.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int PlayCost { get; set; }
        public required string Type { get; set; }
    }
}
