using GTANetworkAPI;

namespace ServerSide
{
    public class Events : Script
    {
        [ServerEvent(Event.PlayerSpawn)]//также как и с командами пишем атрибут, но здесь описывается что сервер-ивент и дальше в скобках при каком тригере происходит ивент 
        public void OnPlayerSpawn(Player player)
        {
            player.Armor = 100;
        }

        [ServerEvent(Event.PlayerEnterVehicle)]//событие когда игрок садиться в т/с
        public void OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatID) 
        {
            if (vehicle == null) return;//проверяем существует машина, если нет то просто возвращаем
            vehicle.PrimaryColor = 12;//если же она есть то красим её в черный цвет(это будет её основным цветом) 
            }

        [ServerEvent(Event.PlayerExitVehicle)]//событие когда игрок покидает т/с.
        public void OnPlayerExitVehicle(Player player, Vehicle vehicle)
        {
            if (vehicle == null) return;//проверяем существует машина, если нет то просто возвращаем
            vehicle.PrimaryColor = 131;//теперь тоже самое только уже перекрашиваем машину в черный цвет
        }

        [RemoteEvent("CLIENT:SERVER:RepairCar")]
        private void RepairCar(Player player)
        {
            if (player.Vehicle == null)
            {
                player.SendChatMessage("You are not have car! Server");
                return;
            }
            player.Vehicle.Repair();
        }

    }
}
