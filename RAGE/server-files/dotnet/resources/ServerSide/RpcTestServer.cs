using System;
using GTANetworkAPI;
using RAGE;

namespace ServerSide
{
    public class RpcTestServer : Script
    {
        private object GetTime(Player player)
        {
            return DateTime.Now;
        }
    }
}
