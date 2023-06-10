using System;
using GTANetworkAPI;

namespace ServerSide
{
    public class RpcTestServer : Script
    {

        private const string GetTimeServerKey = "RPC::CLIENT:SERVER:GetTimeServer";//делаем такой же ключ как и в RpcTestClient для работы с процедурой 


        [RemoteProc(GetTimeServerKey)]//помечаем метод ниже как и с ивентами только теперь пишем RemoteProc
        private string GetTime(Player player)//тест метод для передачи серверного времени на клиент
        {
            return NAPI.Util.ToJson(DateTime.Now);
        }
    }
}
//для теста процедур rage mp