namespace RestoringHP
{
    public class Player
    {
        public string name { get; set; }
        public int HP { get; set; }
        public int hunger { get; set; }
        public string status { get; set; }

        public Player(string name)
        {
            this.name = name;
            HP = 100;
            hunger = 100;
            status = "alive";
        }
        public void GetPlayer()
        {
            Console.WriteLine($"{name} {status}(HP: {HP} Голод: {hunger} )");
        }
    }
}
