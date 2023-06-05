using GTANetworkAPI;

namespace ServerSide
{
    internal class PrivateVehicle : Script
    {
        //делаем ключ для хранения и получения информации о транспорте
        private static string _vehicleKey = ":OwnVehicle";

        [Command]
        private void MyOwnVehicle(Player player)
        {
            //когда игрок спавнит машину мы её создаём
            var Vehicle = NAPI.Vehicle.CreateVehicle(VehicleHash.Adder, player.Position, player.Rotation, 131, 131);   
            Vehicle.SetData(_vehicleKey, player.SocialClubId); //у нас есть entity машины и нам нужно задать ему информацию. В SetData мы передаём ключ где будет храниться значение по которому эта машина будет привязана(в нашем случае это id socialclub игрока который заспавнил эту машину
        }


        //здесь когда рандомный игрок садиться вe)] машину мы проверяем его ключ (с значением его SocialClubId), и если он совпадает с SocialClubId машины то мы его просто выкидываем   
        [ServerEvent(Event.PlayerEnterVehicle)]
        private void OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatId)
        {
            if (vehicle.HasData(_vehicleKey) && vehicle.GetData<ulong>(_vehicleKey) != player.SocialClubId)
            {
                player.SendChatMessage("It's not your car!");
                player.WarpOutOfVehicle();//этим методом мы выселяем игрока из машины
            }
        }

    }
}
