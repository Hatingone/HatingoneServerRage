using System;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;

namespace ClientSide
{
    public class RpcTestClient : Events.Script
    {

        private const string _serverTime = nameof(_serverTime);

         public RpcTestClient() 
        {
            Input.Bind(VirtualKeys.F2, true, GetTimeServer);
        } 

        private async void GetTimeServer()
        {

        }
    }
}
