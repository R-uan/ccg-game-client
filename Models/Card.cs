namespace GameClient.Models
{
    public class Card
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Description { get; set; } // Card description of what it do

        public required int Rarity { get; set; } // Card rarity (0 -> ?) 
        public required int ManaCost { get; set; } // How much mana does the player needs
        public required string Category { get; set; } // Creature, Spell, Enchantment...

        public int? Attack { get; set; } // Base attack (only for creatures)
        public int? Health { get; set; } // Base health (only for creatures)

        // Lua functions that will be triggered on event
        public required List<String> OnPlay { get; set; } // When the card is played on the board.
        public required List<String> OnDraw { get; set; } // When the card is drawn by the owner
        public required List<String> OnDeath { get; set; } // When the card dies
        public required List<String> OnAttack { get; set; } // When the card attacks
        public required List<String> OnTurnEnd { get; set; } // When the owner of the card turn ends
        public required List<String> OnDamaged { get; set; } // When taking damage
        public required List<String> OnTurnStart { get; set; } // When the turn of the owner of the card starts
        public required List<String> OnAllyDeath { get; set; } // When an ally creature dies
        public required List<String> OnEnemyDeath { get; set; } // When an enemy dies
    }
}
