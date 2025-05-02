namespace GameClient.Core
{
    public class MatchService
    {
        public static void ConnectPlayer(Guid playerId, Guid deckId, string token)
        {
            SynapseNet.connect_player(playerId.ToString(), deckId.ToString(), token);
        }
    }
}