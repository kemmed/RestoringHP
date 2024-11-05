using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoringHP
{
    public class FoodManager
    {
        public List<Food> foods;

        public FoodManager()
        {
            foods = new List<Food>
            {
                new Food("Яблоко", 5, 10),
                new Food("Мухомор", -10, 10),
                new Food("Хлеб", 15, 20),
                new Food("Рыба фугу", -5, 15),
                new Food("Зелье восстановления", 50, 0),
                new Food("Вода", 5, 5),
                new Food("Яблочный пирог", 25, 30),
                new Food("Червивое яблоко", -10, 10),
                new Food("Перец Чили", -30, -20)
            };
        }
        public void Eat(Player player, string foodName)
        {
            var food = foods.Find(f => f.name.Equals(foodName, StringComparison.OrdinalIgnoreCase));
            
                if (player.status == "мертв")
                {
                    Console.WriteLine($"Уже не поможет. Игрок {player.name} умер.");
                    return;
                }

                if (player.hunger <= 0)
                {
                    Console.WriteLine($"Игрок {player.name} наелся :Р");
                    return;
                }

                player.hunger -= food.hungerRestoration;
                player.HP += food.HPRestoration;
                Console.WriteLine($"Игрок {player.name} съел {food.name}. (HP: {player.HP}, Голод: {player.hunger})");

            if (player.hunger < 0) 
                    player.hunger = 0;
                else if(player.hunger > 100)
                    player.hunger = 100;
            if (player.HP > 100) 
                    player.HP = 100;
                else if (player.HP < 0) 
                    player.HP = 0;

                if (player.HP <= 0)
                {
                    Console.WriteLine($"Игрок {player.name} отравился и умер :(");
                    player.status = "мертв";
                }

                if (player.hunger <= 0)
                    Console.WriteLine($"Игрок {player.name} наелся :Р");                
        }
        public List<Food> GetFoods()
        {
            return foods;
        }
    }
}
