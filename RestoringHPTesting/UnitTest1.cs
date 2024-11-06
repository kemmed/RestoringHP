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
            player = new Player("“естовый »грок");
        }

        /// <summary>
        /// “ест провер€ет, что при поедании здоровой пищи увеличиваетс€ здоровье (HP) 
        /// и уменьшаетс€ голод (hunger) на соответствующие значени€ восстановлени€ еды.
        /// </summary>
        [Fact]
        public void EatingHealthyFood()
        {
            var foodName = "яблоко";
            var foodHungerRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).hungerRestoration;
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;
            player.HP = 100 - foodHPRestoration;
            var initialHunger = player.hunger;

            foodManager.Eat(player, foodName);

            Assert.Equal(100, player.HP);
            Assert.Equal(initialHunger - foodHungerRestoration, player.hunger);
        }

        /// <summary>
        /// “ест провер€ет, что при поедании €довитой пищи здоровье (HP) уменьшаетс€ на
        /// соответствующие значени€ восстановлени€ еды.
        /// </summary>
        [Fact]
        public void EatingPoisonousFood()
        {
            var initialHP = player.HP;
            var foodName = "ћухомор";
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;

            foodManager.Eat(player, foodName);

            Assert.Equal(initialHP + foodHPRestoration, player.HP);
        }

        /// <summary>
        /// “ест провер€ет, что если здоровье (HP) игрока падает ниже нул€, 
        /// статус игрока мен€етс€ на "dead".
        /// </summary>
        [Fact]
        public void HPDropsBelowZero()
        {
            player.HP = 1;
            var foodName = "ѕерец „или";

            foodManager.Eat(player, foodName);

            Assert.Equal("dead", player.status);
        }

        /// <summary>
        /// “ест провер€ет, что если игрок dead, его здоровье (HP) не измен€етс€ 
        /// при поедании любой пищи.
        /// </summary>
        [Fact]
        public void HPWhenPlayerIsDead()
        {
            player.status = "dead";
            var initialHP = player.HP;
            var foodName = "’леб";

            foodManager.Eat(player, foodName);

            Assert.Equal(initialHP, player.HP);
        }

        /// <summary>
        /// “ест провер€ет, что уровень голода (hunger) не превышает
        /// максимального значени€ после поедани€ пищи.
        /// </summary>
        [Fact]
        public void HungerNotAbove100()
        {
            player.hunger = 95;
            var foodName = "ѕерец „или";

            foodManager.Eat(player, foodName);

            Assert.Equal(100, player.hunger);
        }

        /// <summary>
        /// “ест провер€ет, что уровень голода (hunger) не опускаетс€ ниже 0
        /// после поедани€ пищи.
        /// </summary>
        [Fact]
        public void HungerNotBelow0()
        {
            player.hunger = 5;
            var foodName = "–ыба фугу";

            foodManager.Eat(player, foodName);

            Assert.Equal(0, player.hunger);
        }
    }
}