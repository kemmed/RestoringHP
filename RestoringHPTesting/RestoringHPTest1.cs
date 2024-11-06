namespace RestoringHPTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestoringHP;

    [TestClass]
    public class FoodManagerTests
    {
        private FoodManager foodManager;
        private Player player;

        [TestInitialize]
        public void Setup()
        {
            foodManager = new FoodManager();
            player = new Player("“естовый »грок");
        }

        /// <summary>
        /// “ест провер€ет, что при поедании здоровой пищи увеличиваетс€ здоровье (HP) 
        /// и уменьшаетс€ голод (hunger) на соответствующие значени€ восстановлени€ еды.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldIncreaseHP_WhenEatingHealthyFood()
        {
            var foodName = "яблоко";
            var foodHungerRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).hungerRestoration;
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;
            player.HP = 100 - foodHPRestoration;
            var initialHunger = player.hunger;

            foodManager.Eat(player, foodName);
            
            Assert.AreEqual(100, player.HP);
            Assert.AreEqual(initialHunger - foodHungerRestoration, player.hunger);
        }

        /// <summary>
        /// “ест провер€ет, что при поедании €довитой пищи здоровье (HP) уменьшаетс€ на
        /// соответствующие значени€ восстановлени€ еды.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldDecreaseHP_WhenEatingPoisonousFood()
        {
            var initialHP = player.HP;
            var foodName = "ћухомор";
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;

            foodManager.Eat(player, foodName);
            
            Assert.AreEqual(initialHP + foodHPRestoration, player.HP);
        }

        /// <summary>
        /// “ест провер€ет, что если здоровье (HP) игрока падает ниже нул€, 
        /// статус игрока мен€етс€ на "мертв".
        /// </summary>
        [TestMethod]
        public void Eat_ShouldSetPlayerStatusToDead_WhenHPDropsBelowZero()
        {
            player.HP = 1;
            var foodName = "ѕерец „или";

            foodManager.Eat(player, foodName);

            Assert.AreEqual("мертв", player.status);
        }

        /// <summary>
        /// “ест провер€ет, что если игрок мертв, его здоровье (HP) не измен€етс€ 
        /// при поедании любой пищи.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldNotChangeHP_WhenPlayerIsDead()
        {
            player.status = "мертв";
            var initialHP = player.HP;
            var foodName = "’леб";

            foodManager.Eat(player, foodName);

            Assert.AreEqual(initialHP, player.HP);
        }

        /// <summary>
        /// “ест провер€ет, что уровень голода (hunger) не превышает
        /// максимального значени€ после поедани€ пищи.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldNotIncreaseHungerAbove100()
        {
            player.hunger = 95;
            var foodName = "ѕерец „или"; 

            foodManager.Eat(player, foodName); 

            Assert.AreEqual(100, player.hunger);
        }

        /// <summary>
        /// “ест провер€ет, что уровень голода (hunger) не опускаетс€ ниже 0
        /// после поедани€ пищи.
        /// </summary>
        [TestMethod] 
        public void Eat_ShouldNotDecreaseHungerBelow0()
        {
            player.hunger = 5;
            var foodName = "–ыба фугу";

            foodManager.Eat(player, foodName);

            Assert.AreEqual(0, player.hunger);
        }

        /// <summary>
        /// “ест провер€ет, что метод GetFoods() возвращает 
        /// всю доступную еду в FoodManager().
        /// </summary>
        [TestMethod]
        public void GetFoods_ShouldReturnAllAvailableFoods()
        {
            var foods = foodManager.GetFoods();

            Assert.IsNotNull(foods);
            Assert.AreEqual(9, foods.Count);
        }
    }

    [TestClass]
    public class PlayerTests
    {
        /// <summary>
        /// “ест провер€ет, что объект Player инициализируетс€ с 
        /// правильными значени€ми при создании нового игрока.
        /// </summary>
        [TestMethod]
        public void Player_ShouldInitializeWithCorrectValues()
        {
            string playerName = "»грок1";

            var player = new Player(playerName);

            Assert.AreEqual(playerName, player.name);
            Assert.AreEqual(100, player.HP);
            Assert.AreEqual(100, player.hunger);
            Assert.AreEqual("жив", player.status);
        }
    }
}
