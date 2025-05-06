using System.Runtime.InteropServices;

namespace GameClient.Core
{
    public static class SynapseNet
    {
        [DllImport("libSynapseNet")]
        public extern static void start_connection(string addr, int port);

        [DllImport("libSynapseNet")]
        public extern static void connect_player(string playerId, string playerDeckId, string token);

        [DllImport("libSynapseNet")]
        public extern static IntPtr retrieve_gamestate(out int outSize);

        [DllImport("libSynapseNet")]
        public extern static void free_ptr(IntPtr ptr);

        [DllImport("libSynapseNet")]
        public extern static IntPtr send_packet(IntPtr messageType, string payload, int length);

        [DllImport("libSynapseNet")]
        public extern static IntPtr retrieve_error(out int outSize);
    }
}