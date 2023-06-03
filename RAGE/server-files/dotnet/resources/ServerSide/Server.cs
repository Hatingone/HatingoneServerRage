using GTANetworkAPI;
using RAGE;
using System;

namespace ServerSide
{
    public class Main : Script //Наследуем класс "Script" у GTANetworkAPI для дальнейшей работы
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()//Пишем тестовый метод для проверки работы сервера при запуске. В данном случае когда мы запускаем сервер, он при успешном запуске выдаёт сообщение: "Server started and ready for work"
        {
            Console.WriteLine("Server started, and ready for work!");
        }


    }
}
