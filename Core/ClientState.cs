using GameClient.Models;

namespace GameClient.Core
{
    public class ClientState
    {
        public bool IsLogged { get; set; } = false;
        public List<Deck>? PlayerDecks { get; set; }
        public List<Card>? CardCollection { get; set; }
        public PlayerProfile? PlayerProfile { get; set; }
    }
}
