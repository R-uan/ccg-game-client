using System.Runtime.InteropServices;

namespace GameClient.Core
{
    public static class SynapseNet
    {
        [DllImport("libSynapseNet")]
        public extern static void start_connection(string addr, int port);
    }
}