using System;
using GTANetworkAPI;

namespace ServerSide
{
    internal class Level : Script
    {

        private static string _levelKey = nameof(_levelKey);

        [ServerEvent(Event.PlayerConnected)]
        private void OnPlayerConnected(Player player)
        {
            player.SetSharedData(_levelKey, new Random().Next(1, 100));//когда игрок присоединяется ему выдаётся рандомный уровень от 1 до 100 
        }
    }
}
//здесь у нас будет прописан лвл игрока. Здесь будет почти такой же принцип что и с PrivateVehicle(ключ и т.д.)