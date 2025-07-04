using System.Runtime.InteropServices;
using GameClient.Enums;
using GameClient.Models;
using PeterO.Cbor;

namespace GameClient.Core
{
    public static class SynapseNet
    {
        [DllImport("libSynapseNet")]
        private static extern int start_connection(string addr, int port);

        [DllImport("libSynapseNet")]
        private static extern int connect_player(string playerId, string playerDeckId, string token);
        
        [DllImport("libSynapseNet")]
        private static extern int reconnect_player(string playerId, string token);

        [DllImport("libSynapseNet")]
        private static extern IntPtr retrieve_game_state(out int outSize);

        [DllImport("libSynapseNet")]
        private static extern IntPtr retrieve_error(out int outSize);
        
        [DllImport("libSynapseNet")]
        private static extern void free_ptr(IntPtr ptr);
        
        [DllImport("libSynapseNet")]
        private static extern int play_card(string cardId, string actorId, string? targetId, string? targetPosition);

        public static bool PlayCard(Guid cardId, Guid actorId, Guid? targetId, string? targetPosition)
        {
            // This is the object that will be converted into CBOR bytes.
            // For now, it will be an anonymous object as the composition is
            // pretty simple and won't be required to be deserialized into a C# object later.
            var bytesSent = SynapseNet.play_card(cardId.ToString()!, actorId.ToString()!, targetId.ToString(), targetPosition);
            return bytesSent > 0;
        }
        
        public static bool ConnectPlayerToMatchServer(MatchConnectionInfo info)
        {
            var bytesSend = SynapseNet.connect_player(info.PlayerId, info.CurrentDeckId, info.PlayerAuthToken);
            return bytesSend > 0;
        }
        
        public static bool ReconnectPlayerToMatchServer(MatchReconnectionInfo info)
        {
            var bytesSend = SynapseNet.reconnect_player(info.PlayerId, info.PlayerAuthToken);
            return bytesSend > 0;
        }
        
        // The address of the server will be dynamic later on.
        public static int ConnectToServer()
            => SynapseNet.start_connection("127.0.0.1", 8000);

        public static Result<byte[]> RetrieveError()
        {
            IntPtr ptr = SynapseNet.retrieve_error(out int size);
            if (ptr == IntPtr.Zero)
                return Result<byte[]>.Fail("No Error packet was retrieved.");
            
            if (size <= 0)
                return Result<byte[]>.Fail("Packet retrieved had no data");

            byte[] bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            SynapseNet.free_ptr(ptr);
            
            return Result<byte[]>.Ok(bytes);
        }
        
        public static Result<byte[]> RetrieveGameState()
        {
            IntPtr ptr = SynapseNet.retrieve_game_state(out int size);
            if (ptr == IntPtr.Zero)
                return Result<byte[]>.Fail("No Game State packet was retrieved.");
            
            if (size <= 0)
                return Result<byte[]>.Fail("Packet retrieved had no data");

            byte[] bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            SynapseNet.free_ptr(ptr);
            
            return Result<byte[]>.Ok(bytes);
        }
    }
}