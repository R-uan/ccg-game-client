using System.Runtime.InteropServices;

namespace GameClient.Core
{
    public static class SynapseNet
    {
        [DllImport("libSynapseNet")]
        public extern static void start_connection(string addr, int port);

        [DllImport("libSynapseNet")]
        public extern static void connect_player(string playerId, string playerDeckId, string token);
    }
}