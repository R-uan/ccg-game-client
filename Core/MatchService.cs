using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;

namespace GameClient.Core
{
    public class MatchService
    {
        public static void ConnectPlayer(Guid playerId, Guid deckId, string token)
        {
            SynapseNet.connect_player(playerId.ToString(), deckId.ToString(), token);
        }

        public static void ReadGameState()
        {
            IntPtr ptr = SynapseNet.retrieve_gamestate(out int size);
            if (ptr == IntPtr.Zero)
            {
                Logger.Info("No Game State packet was retrieved");
                return;
            }

            if (size <= 0)
            {
                Logger.Info("No bytes were read ...");
                return;
            }

            byte[] bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            SynapseNet.free_ptr(ptr);
        }
    }
}