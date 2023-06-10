using System;
using RAGE; 
using RAGE.Elements;
using RAGE.Ui;

namespace ClientSide
{
    public class RpcTestClient : Events.Script
    {

        private const string GetTimeServerKey = "RPC::CLIENT:SERVER:GetTimeServer";//ключ для вызова процедуры. Здесь принцип очень схож с ивентами. У нас здесь тоже есть ключ но он для вызова процедуры. Здесь порядок наименования схож с тем что я сделал в файле Events, в методе RepairCar: ЛюбоеНазавание::КтоВызываетПроцедуру::КтоЕёОбрабатывает::ЧтоНужноСделатьИлиНазваниеПроцедуры. У меня это выглядит так "RPC::CLIENT:SERVER:GetTimeServer"

        public RpcTestClient() 
        {
            Input.Bind(VirtualKeys.F2, true, GetTimeServer);//бинд для вызова просмотра серверного времени
        } 

        private async void GetTimeServer()//метод для получения серверного времени. Делаем его ассинхронным для получения ответа от сервера.
        {
            //делаем обработку
            var answer = (string) await Events.CallRemoteProc(GetTimeServerKey);//получаем ответ от того что мы вызовем процедуру через CallRemoteProc, ждем ответ(await), и переводим в строку. Процедуры вызываются также как и ивенты. Поэтому в скобках пишем ключ также как и в ивентах. И превращаем это всё в string и делаем await так как это ассинх. метод 
            //получаем дату
            var date = RAGE.Util.Json.Deserialize<DateTime>(answer);//строку которую получили вверху десериализовываем с помощью RAGE.Util.Json в DateTime
            //выводим в чат игрока время
            Chat.Output(text: $"Time server = {date.ToShortTimeString()}");
        }
    }
}
