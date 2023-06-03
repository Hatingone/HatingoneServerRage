using GTANetworkAPI;


namespace ServerSide.CommandAttributes
{
    internal class RequiresHealthAttribute : CommandConditionAttribute
    {
        public int Amount { get; set; }

        public RequiresHealthAttribute(int amount = 100)
        {
            Amount = amount;
        }

        public override bool Check(Player player, string cmdName, string cmdText)
        {
            //пишем ту же самую логику что и у команды healme(только это кастомный атрибут к той команде, и там же можно посмотреть как мы его подключили). И это все мы сделили как замену той конструкции if которую мы делали до этого в команде. Да бы сделать код красивей
            if (player.Health > Amount)//если здоровье игрока больше чем Amount(который равен 100) и пишем ему что он не вылечен и возвращаем значение false(тоесть проверка не пройдена или по другому можно сказать что это отказ и не даем право на использование команды), в другом случае возвращаем true(тоесть мы даём право на использование команды)
            {
                player.SendChatMessage("No heal for you bro! You're heal af");
                return false;
            }
            return true;
        }
    }
}
