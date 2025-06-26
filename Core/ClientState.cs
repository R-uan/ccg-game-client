using GameClient.Models;

namespace GameClient.Core
{
    public class ClientState
    {
        public string? CurrentDeck { get; set; }
        
        public bool IsLogged { get; set; } = false;
        public List<Deck>? PlayerDecks { get; set; }
        public List<Card>? CardCollection { get; set; }
        public PlayerAccount? PlayerAccount { get; set; }
    }
}
