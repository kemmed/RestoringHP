namespace RestoringHP
{
    internal class Game
    {
        private FoodManager foodManager;
        private Player player;

        public Game(string playerName)
        {
            player = new Player(playerName);
            foodManager = new FoodManager();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Поесть");
                Console.WriteLine("2. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EatMenu();
                        break;

                    case "2":
                        return;

                    default:
                        Console.WriteLine("Выберите действие.");
                        break;
                }
            }
        }
        private void EatMenu()
        {
            while (true)
            {
                Console.Clear();
                player.GetPlayer();
                Console.WriteLine("Доступная еда:");
                var foods = foodManager.GetFoods();
                for (int i = 0; i < foods.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {foods[i].name} (HP: {foods[i].HPRestoration}, Утоление голода: {foods[i].hungerRestoration})");
                }
                Console.WriteLine();
                Console.WriteLine("Введите номер еды из списка (или 0 для возврата в главное меню):");

                string input = Console.ReadLine();

                if (input == "0")
                    return;

                if (int.TryParse(input, out int foodIndex) && foodIndex > 0 && foodIndex <= foods.Count)
                {
                    var selectedFood = foods[foodIndex - 1];
                    foodManager.Eat(player, selectedFood.name);
                }
                else
                {
                    Console.WriteLine("Неверный номер еды. Выберите еду из списка (или 0 для возврата в главное меню).");
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}
