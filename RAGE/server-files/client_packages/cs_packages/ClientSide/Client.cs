using RAGE;
using System;
using RAGE.Elements;
using RAGE.Ui;

namespace ClientSide
{
    public class Client : Events.Script
    {

        private const string _levelKey = nameof(_levelKey);// создаём такой же ключ как и в Level для того чтобы передать его в методе LevelAllPlayers как значение уровня у игрока 

        public Client()
        {
            RAGE.Chat.Output("Hello World! Client");
            RAGE.Input.Bind(VirtualKeys.N, true, LevelAllPlayers);//при нажатии кнопки N у нас вызывается метод LevelAllPlayers
        }

        private void LevelAllPlayers ()
        {
            foreach (var player in RAGE.Elements.Entities.Players.All)//здесь мы обращаемся к классу Entities чтобы потом обратиться к Players, а дальше с помощью All преобразовать всё это в list. Чтобы нам выдало список игроков с уровнями.
            {
                Chat.Output(player.Name + "------" + player._GetSharedData<int>(_levelKey) + "lvl");//выводим в чат список игроков: их имена и лвл
            }
        }
    }
}