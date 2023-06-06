using RAGE;
using System;
using RAGE.Elements;
using RAGE.Ui;

namespace ClientSide
{
    public class Client : Events.Script
    {

        private const string _levelKey = nameof(_levelKey);//создаём такой же ключ как и в Level для того чтобы передать его в методе LevelAllPlayers как значение уровня у игрока 
        private const string _walletKey = nameof(_walletKey);//создаём такой же ключ как и в Wallet для того чтобы передать его в DataHandler как значение денег у игрока

        public Client()
        {
            RAGE.Chat.Output("Hello World! Client");
            RAGE.Input.Bind(VirtualKeys.N, true, LevelAllPlayers);//при нажатии кнопки N у нас вызывается метод LevelAllPlayers
            //когда изменяется значение на сервере нам надо как-то оповестить клиента об этом. И у нас есть такая штука как DataHandler. Он сам отслеживает изменение значение которое нам надо. И если изменение значения произойдёт, то DataHandler вызовет определённый метод который мы ему скажем. 
            RAGE.Events.AddDataHandler(_walletKey, HandlerMoney);//сначала прописываем ключ по которому он будет отслеживать значение, а потом прописываем название метода который будет вызываться при изминении значения. 
        }

        private void HandlerMoney (Entity entity, object arg, object oldarg)
        {
            //сначала проверяем точно ли entity (у которого поменялось значение) является игроком
            if (entity is RAGE.Elements.Player == false) return;
            //здесь мы "превращаем" arg и oldarg в money и OldMoney для удобства
            int Money = (int)arg;
            int OldMoney = (int)oldarg;

            if (Money > OldMoney)//если произошло добавление денег то мы пишем игроку что он заработал дениги, а затем выводим количество которое мы заработали 
            {
                Chat.Output("You earned money. Amount: " + (Money - OldMoney));
            }
            else //здесь происходит наоборот. Когда игрок потратил деньги. 
            {
                Chat.Output("You spent money. Amount:"  + (OldMoney - Money));
            }
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