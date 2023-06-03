using RAGE;
using RAGE.Elements;
using System;

namespace ClientSide.Player
{
    internal class ExampleEvents : Events.Script//для начала нам нужно унаследовать Events.Script для того чтобы использовать любые ивенты, команды и т. п. именно на клиентсайде(для серверсайда наследование другое)
    {
        public ExampleEvents()//здесь будет происходить регистрация ивентов
        {
            Events.OnPlayerEnterColshape += OnPlayerEnterColshape;
            Events.OnPlayerExitColshape += OnPlayerExitColshape;
            Events.Add("SERVER:CLIENT:randomize", randomizePlayer);//регистрируем ивент randomize
            RAGE.Input.Bind(RAGE.Ui.VirtualKeys.F5, true, () => //этот ивент будет чинить машину на кнопку F5
            {
                if (RAGE.Elements.Player.LocalPlayer.Vehicle == null)//проверяем есть ли у игрока машина, дабы лишний раз не вызывать ивент
                {
                    RAGE.Chat.Output("You are not have car! Client"); 
                    return;
                }
                Events.CallRemote("CLIENT:SERVER:RepairCar");//мы также можем использовать тот метод тригера ивента с randomize только там тригер был с сервера на клиент, а здесь с клиента на сервер
            });
        }

        private void randomizePlayer(object[] args)
        {
            Random random = new Random();
            RAGE.Elements.Player.LocalPlayer.SetHeadBlendData(
                random.Next(0, 3),
                random.Next(0, 3),
                random.Next(0, 3),
                random.Next(0, 3),
                random.Next(0, 3),
                random.Next(0, 3),
                0.5f,
                0.5f,
                0.5f,
                false);//здесь меняем хэндблэд игрока
        }

        private void OnPlayerExitColshape(Colshape colshape, Events.CancelEventArgs cancel)
        {
            RAGE.Chat.Output("You exit the colshape");
        }

        private void OnPlayerEnterColshape(Colshape colshape, Events.CancelEventArgs cancel)
        {
            RAGE.Chat.Output("You enter the colshape");
        }
    }
}
//различие между клиентсайд ивентами и серверсайд ивентами в том что в клиентсайде мы не используем атрибуты, а должны регестрировать ивенты сами в конструкторе класса
