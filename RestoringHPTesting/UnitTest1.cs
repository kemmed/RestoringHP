using RestoringHP;
using System.Linq;
using Xunit;

namespace RestoringHPTesting
{
    public class FoodManagerTests
    {
        private readonly FoodManager foodManager;
        private readonly Player player;

        public FoodManagerTests()
        {
            foodManager = new FoodManager();
            player = new Player("Тестовый Игрок");
        }

        /// <summary>
        /// Тест проверяет, что при поедании здоровой пищи увеличивается здоровье (HP) 
        /// и уменьшается голод (hunger) на соответствующие значения восстановления еды.
        /// </summary>
        [Fact]
        public void EatingHealthyFood()
        {
            var foodName = "Яблоко";
            var foodHungerRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).hungerRestoration;
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;
            player.HP = 100 - foodHPRestoration;
            var initialHunger = player.hunger;

            foodManager.Eat(player, foodName);

            Assert.Equal(100, player.HP);
            Assert.Equal(initialHunger - foodHungerRestoration, player.hunger);
        }

        /// <summary>
        /// Тест проверяет, что при поедании ядовитой пищи здоровье (HP) уменьшается на
        /// соответствующие значения восстановления еды.
        /// </summary>
        [Fact]
        public void EatingPoisonousFood()
        {
            var initialHP = player.HP;
            var foodName = "Мухомор";
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;

            foodManager.Eat(player, foodName);

            Assert.Equal(initialHP + foodHPRestoration, player.HP);
        }

        /// <summary>
        /// Тест проверяет, что если здоровье (HP) игрока падает ниже нуля, 
        /// статус игрока меняется на "dead".
        /// </summary>
        [Fact]
        public void HPDropsBelowZero()
        {
            player.HP = 1;
            var foodName = "Перец Чили";

            foodManager.Eat(player, foodName);

            Assert.Equal("dead", player.status);
        }

        /// <summary>
        /// Тест проверяет, что если игрок dead, его здоровье (HP) не изменяется 
        /// при поедании любой пищи.
        /// </summary>
        [Fact]
        public void HPWhenPlayerIsDead()
        {
            player.status = "dead";
            var initialHP = player.HP;
            var foodName = "Хлеб";

            foodManager.Eat(player, foodName);

            Assert.Equal(initialHP, player.HP);
        }

        /// <summary>
        /// Тест проверяет, что уровень голода (hunger) не превышает
        /// максимального значения после поедания пищи.
        /// </summary>
        [Fact]
        public void HungerNotAbove100()
        {
            player.hunger = 95;
            var foodName = "Перец Чили";

            foodManager.Eat(player, foodName);

            Assert.Equal(100, player.hunger);
        }

        /// <summary>
        /// Тест проверяет, что уровень голода (hunger) не опускается ниже 0
        /// после поедания пищи.
        /// </summary>
        [Fact]
        public void HungerNotBelow0()
        {
            player.hunger = 5;
            var foodName = "Рыба фугу";

            foodManager.Eat(player, foodName);

            Assert.Equal(0, player.hunger);
        }
    }
}
