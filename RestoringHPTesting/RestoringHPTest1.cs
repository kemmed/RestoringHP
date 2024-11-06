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
            player = new Player("�������� �����");
        }

        /// <summary>
        /// ���� ���������, ��� ��� �������� �������� ���� ������������� �������� (HP) 
        /// � ����������� ����� (hunger) �� ��������������� �������� �������������� ���.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldIncreaseHP_WhenEatingHealthyFood()
        {
            var foodName = "������";
            var foodHungerRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).hungerRestoration;
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;
            player.HP = 100 - foodHPRestoration;
            var initialHunger = player.hunger;

            foodManager.Eat(player, foodName);
            
            Assert.AreEqual(100, player.HP);
            Assert.AreEqual(initialHunger - foodHungerRestoration, player.hunger);
        }

        /// <summary>
        /// ���� ���������, ��� ��� �������� �������� ���� �������� (HP) ����������� ��
        /// ��������������� �������� �������������� ���.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldDecreaseHP_WhenEatingPoisonousFood()
        {
            var initialHP = player.HP;
            var foodName = "�������";
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;

            foodManager.Eat(player, foodName);
            
            Assert.AreEqual(initialHP + foodHPRestoration, player.HP);
        }

        /// <summary>
        /// ���� ���������, ��� ���� �������� (HP) ������ ������ ���� ����, 
        /// ������ ������ �������� �� "�����".
        /// </summary>
        [TestMethod]
        public void Eat_ShouldSetPlayerStatusToDead_WhenHPDropsBelowZero()
        {
            player.HP = 1;
            var foodName = "����� ����";

            foodManager.Eat(player, foodName);

            Assert.AreEqual("�����", player.status);
        }

        /// <summary>
        /// ���� ���������, ��� ���� ����� �����, ��� �������� (HP) �� ���������� 
        /// ��� �������� ����� ����.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldNotChangeHP_WhenPlayerIsDead()
        {
            player.status = "�����";
            var initialHP = player.HP;
            var foodName = "����";

            foodManager.Eat(player, foodName);

            Assert.AreEqual(initialHP, player.HP);
        }

        /// <summary>
        /// ���� ���������, ��� ������� ������ (hunger) �� ���������
        /// ������������� �������� ����� �������� ����.
        /// </summary>
        [TestMethod]
        public void Eat_ShouldNotIncreaseHungerAbove100()
        {
            player.hunger = 95;
            var foodName = "����� ����"; 

            foodManager.Eat(player, foodName); 

            Assert.AreEqual(100, player.hunger);
        }

        /// <summary>
        /// ���� ���������, ��� ������� ������ (hunger) �� ���������� ���� 0
        /// ����� �������� ����.
        /// </summary>
        [TestMethod] 
        public void Eat_ShouldNotDecreaseHungerBelow0()
        {
            player.hunger = 5;
            var foodName = "���� ����";

            foodManager.Eat(player, foodName);

            Assert.AreEqual(0, player.hunger);
        }

        /// <summary>
        /// ���� ���������, ��� ����� GetFoods() ���������� 
        /// ��� ��������� ��� � FoodManager().
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
        /// ���� ���������, ��� ������ Player ���������������� � 
        /// ����������� ���������� ��� �������� ������ ������.
        /// </summary>
        [TestMethod]
        public void Player_ShouldInitializeWithCorrectValues()
        {
            string playerName = "�����1";

            var player = new Player(playerName);

            Assert.AreEqual(playerName, player.name);
            Assert.AreEqual(100, player.HP);
            Assert.AreEqual(100, player.hunger);
            Assert.AreEqual("���", player.status);
        }
    }
}
