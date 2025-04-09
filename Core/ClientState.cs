namespace GameClient.Core
{
    public class ClientState
    {
        public bool IsLogged { get; set; } = false;
        public List<Deck>? PlayerDecks { get; set; }
    }
}
