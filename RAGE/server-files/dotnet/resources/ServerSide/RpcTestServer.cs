using System;
using System.Linq;
using GTANetworkAPI;

namespace ServerSide
{
    public class RpcTestServer : Script
    {

        private const string GetTimeServerKey = "RPC::CLIENT:SERVER:GetTimeServer";//делаем такой же ключ как и в RpcTestClient для работы с процедурой 
        private const string GetFpsPlayerKey = "RPC::SERVER:CLIENT:GetFPSClient";//клбч для работы с фпс игрока

        [RemoteProc(GetTimeServerKey)]//помечаем метод ниже как и с ивентами только теперь пишем RemoteProc
        private string GetTime(Player player)//тест метод для передачи серверного времени на клиент
        {
            return NAPI.Util.ToJson(DateTime.Now);
        }

        [Command]
        private void GetFPSPlayer(Player player, int id)
        {
            var playerFps = NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.Id == id);//нам нужно получить игрока через пул GetAllPlayers. И затем с помощью FirstOrDefault в котором будем искать объект по айдишнику
            if (playerFps == null)//проверяем есть ли игрок 
            {
                player.SendChatMessage("Player not found");
                return;
            }
            //так как любой вызов процедуры должен быть ассинхронный, то запускаем новый task чтобы не делать метод ассинхроным(запускаем его здесь). И для этого воспользуемся NAPI.Task.Run
            NAPI.Task.Run(async () =>
            {
                //и дальше говорим что здесь будем делать
                var answer = (float) await playerFps.TriggerProcedure(GetFpsPlayerKey);//нам нужно получить ответ который будет состоять из того что мы затригерим процедуру(пишем ключ). И мы знаем что нам придёт float(так как до этого делили на 1) поэтому можем сразу преобразовать до него
                //теперь игроку который вызвал эту команду нужно отправить сообщение о его фпс
                player.SendChatMessage($"Player {playerFps.Name} fps = {answer}");
            });
        }
    }
}
//для теста процедур rage mp