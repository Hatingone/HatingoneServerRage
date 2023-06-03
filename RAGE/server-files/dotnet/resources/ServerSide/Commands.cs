using GTANetworkAPI;
using ServerSide.CommandAttributes;

namespace ServerSide
{
    public class Commands : Script //наследуем класс "Script".
    {
        [Command("car")]//с помощью этого атрибута говорим RAGEAPI что метод "CreateCar" является командой
        public void CreateCar(Player player)//создаём метод для создания машины. Первым параметром в этом методе всегда приходит Player(игрок) который вызвал его.   
        {
            NAPI.Vehicle.CreateVehicle(VehicleHash.Adder, player.Position, player.Heading, 131, 131);
        }

        [Command("hp")]//в скобках находиться Alias(синноним), дабы в чат полностью не прописывать название метода для вызова комманды мы можем сами задать какое будет название у нашей команды когда мы прописываем её в чат
        public void SetHealt(Player player, int count)//int count это параметр в который мы можем задать кол-во хп игроку
        {
            player.Health = count;
        }

        [Command("teleport", Alias = "tp")]//телепортироваться можно как по команде "teleport", так и "tp"
        public void TeleportPlayer(Player player, float x, float y, float z)//метод сначала принимает игрока а потом 3 значания типа float(которые будут выступать в роли позиции игрока в пространстве)
        {
            player.Position= new Vector3(x, y, z);//делаем новый вектор так же как и в юнити
        }

        [Command("armor")]
        public void SetPlayerArmor(Player player, int armor = 100)//В некоторых ситуациях нам лень писать параметры или мы хотим чтобы он был изначально задан(Пример(он уже находится в параметрах метода):мы его уже заранее задали в виде брони игрока и изначально у него будет 100 едениц брони int armor = 100) на помощь нам приходят Необязательный параметр(Optional parametrs). Такая же фишка есть и C#
        {
            player.Armor = armor;
        }

        [Command("me", GreedyArg = true)]//делаем команду "/me" как на рп проектах(способ рп отыгровки). У рейджа есть такая проблема то что он парсит значение через пробел(аналог функции split когда мы работаем со строками). И для того чтобы рейдж не делил это на несколько частей, мы вводим параметр GreedyArg(Greedy Argument). Тоесть всё то значение что мы введём будет попадать в один параметр, в нашем случае это строка actions
        public void TypeMe(Player player, string actions)
        {
            player.SendChatMessage($"{player.Name} did that " + actions); //выводим игроку переменную actions когда он что-то сделал(Игрок пишет: /me взял камень в руки. В чат выведится: *Имя игрока* did that + взял камень в руки)
        }

        [RequiresHealth(75)]//это кастомный атрибут который мы сделали в директории(CommandAttributes) с кастомными атрибутами
        [Command("healme")]
        public void HealPlayer(Player player) 
        {
            player.Health = 100;
            player.SendChatMessage("You were healed!");
        }

        [Command("marker")]
        public void Marker(Player player, uint markerType ) //команда которая может создавать маркера
        {
            NAPI.Marker.CreateMarker(markerType, player.Position, new Vector3(), new Vector3(), 2, new Color(255, 0 , 0, 100), false, player.Dimension );//в этой функции описывается тип маркера, его поведение, цвет, размеры и т. д.
        }

        private Checkpoint _prevCheckpoint;
        [Command("checkpoint")]//это команда аналогично верхней только здесь мы создаём чекпоинты, но их отличие в том что чекпоинтам можно задать направление, и в данном случае происходит запоминание последнего чекпоинта в поле, также теперь каждый новый чекпоинт будет смотреть на предыдущий    
        public void Checkpoint(Player player, uint checkpointType) 
        {
            var direction = _prevCheckpoint?.Position ?? player.Position;//делаем направление

            _prevCheckpoint = NAPI.Checkpoint.CreateCheckpoint(checkpointType, player.Position + new Vector3(0f, 0f, -1f), direction, 1f, new Color(255, 0, 0, 100), player.Dimension);//и задаём параметры при создании самого чекпоинта

        }

        [Command("blip")]//делаем команду для создания блипов(метки на карте)
        public void Blip(Player player, uint sprite, byte color, string name, bool shortRange)
        {
            NAPI.Blip.CreateBlip(sprite, player.Position, 1f, color, name, 255, 0f, shortRange, 0, player.Dimension);//и задаём параметры при создании самого блипа. Про параметры: sprite-это айди картинки для метки(все варианты можно посмотреть в rage wiki), scale-это размер метки, byte-это айди цвета(их тоже можно посмотреть в rage wiki), name-имя блипа, byte alpha - прозрачность блипа, drowDistance-почти ни на что не влияет, shortRange - он отвечает за то будет ли виден блип на миникарте если мы далеко от него находимся, rotation - поворот блипа(поворот в градусах), Dimension - эдакое измерение сервера(в данном случае блип будет в дименшоне игрока)
        }

        [Command("colshape")]//это сущность которая выделяет какую-нибуль зону и отслеживает есть в ней кто-то или нет(или просто находится)
        public void Colshape(Player player, float scale) 
        {
            var position = player.Position + new Vector3(0f, 0f, -1f);

            var colShape = NAPI.ColShape.CreateCylinderColShape(position, scale, 2f, player.Dimension);//кратко о параметрах: позиция при создании(position), радиус активности или работы(float range), высота(float height), и сам деменшон(dimension)
            colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, position, new Vector3(), new Vector3(), scale * 2, new Color(255, 0, 0, 100), false, player.Dimension));//дабы было удобно, мы визуально дополнительно спавним маркер на колшейпе. Но когда в игре указываем размер колшейпа он означает радиус, а у маркера это диаметр, поэтому размер у маркера мы умножаем на два дабы радиус колшепа и маркера визуално совпадалиы

            colShape.OnEntityEnterColShape += OnEntityEnterColShape;//объект colShape содержит в себе два ивента это: OnEntityEnterColShape и OnEntityExitColShape. Можно тогадаться по названиям этих событий что будет происходит с игроком когда он войдёт(В это время вызывается OnEntityEnterColShape) в этот колшейп(взависимости от того какие условия для этих событий мы пропишем), или выёдет(В это время вызывается OnEntityExitColShape) с этого колшейпа
            colShape.OnEntityExitColShape += OnEntityExitColShape;
        }

        //в двух методах ниже описывается что будет происходить когда будут вызываться OnEntityEnterColShape и OnEntityExitColShape, когда игрок будет входить или выходить с колшейпа
        private void OnEntityEnterColShape(ColShape colShape, Player player)
        {
            player.Health = 100;
            player.Armor = 100;
        }

        private void OnEntityExitColShape(ColShape colShape, Player player)
        {
            player.Health = 100;
            player.Armor = 0;
        }

        [Command("randomize")]
        private void randomize(Player player)//этот метод будет только тригерить ивент(чтобы создать ивент мы не должны ничего делать, мы должны только его затригерить и обработать)
        {
            player.TriggerEvent("SERVER:CLIENT:randomize");//сначала мы пишем имя того кто будет передавать этот ивент(в нашем случае это SERVER), потом имя того кто реализует этот ивент(в нашем случае это CLIENT), потом что этот ивент будет делать или его название(в нашем случае это Randomize)
        }




    }//Если мы напишем в игровой чат название метода "CreateCar", то он скажет классу "Script" о том что мы вызываем метод "CreateCar", он передаст эту информацию в Commands который вызовет метод "CreateCar".
}
