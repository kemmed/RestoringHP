namespace RestoringHP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя игрока:");
            string playerName = Console.ReadLine();

            Game game = new Game(playerName);
            game.Start();
        }
    }
}
