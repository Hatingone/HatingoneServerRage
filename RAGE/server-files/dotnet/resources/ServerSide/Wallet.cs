using GTANetworkAPI;
using System;


namespace ServerSide
{
    internal class Wallet : Script
    {
        private const string _walletKey = nameof(_walletKey);

        [Command]
        private void MoneyGet(Player player, int amount)
        {
            //делаем проверку на наличие кошелька у игрока. У игрока должен быть кошелёк(он у нас выступает в роли ключа _walletKey) если этого кошелька нет то мы создаём его(через player.SetOwnSharedData и указываем тамже размер кошелька ввиде 0 денег). Перед этим обратившись к как бы несуществующему ключу у игрока.
            if (player.GetOwnSharedData<int?>(_walletKey) == null) player.SetOwnSharedData(_walletKey, 0);
            //а здесь мы описываем процесс начисления денег. Сначала мы должны получить текущее значение денег у игрока, и за этим уже добавить новое значение денег для него которое он получил. 
            player.SetOwnSharedData(_walletKey, player.GetOwnSharedData<int>(_walletKey) + amount);
        }

        [Command]
        private void MoneySpend(Player player, int amount)
        {
            //пишем тоже самое что и в прошлом методе
            if (player.GetOwnSharedData<int?>(_walletKey) == null) player.SetOwnSharedData(_walletKey, 0);

            //проверяем есть ли у него та сумма которую он хочет потратить. Если у игрока недостаточно денег то сообщаем ему об этом и возвращаем значение
            if (player.GetOwnSharedData<int>(_walletKey) < amount)
            {
                player.SendChatMessage("You don't have enough money");
                return;
            }

            //в другом случае игрок тратит деньги и отнимаем у него ту сумму которую он тратил
            player.SetOwnSharedData(_walletKey, player.GetOwnSharedData<int>(_walletKey) - amount);
        }
    }
}//здесь мы будем реализовывать систему кошелька. То есть игрок сможет тратить сумму и пополнять её