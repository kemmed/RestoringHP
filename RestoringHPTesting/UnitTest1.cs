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
            player = new Player("�������� �����");
        }

        /// <summary>
        /// ���� ���������, ��� ��� �������� �������� ���� ������������� �������� (HP) 
        /// � ����������� ����� (hunger) �� ��������������� �������� �������������� ���.
        /// </summary>
        [Fact]
        public void EatingHealthyFood()
        {
            var foodName = "������";
            var foodHungerRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).hungerRestoration;
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;
            player.HP = 100 - foodHPRestoration;
            var initialHunger = player.hunger;

            foodManager.Eat(player, foodName);

            Assert.Equal(100, player.HP);
            Assert.Equal(initialHunger - foodHungerRestoration, player.hunger);
        }

        /// <summary>
        /// ���� ���������, ��� ��� �������� �������� ���� �������� (HP) ����������� ��
        /// ��������������� �������� �������������� ���.
        /// </summary>
        [Fact]
        public void EatingPoisonousFood()
        {
            var initialHP = player.HP;
            var foodName = "�������";
            var foodHPRestoration = foodManager.foods.FirstOrDefault(x => x.name == foodName).HPRestoration;

            foodManager.Eat(player, foodName);

            Assert.Equal(initialHP + foodHPRestoration, player.HP);
        }

        /// <summary>
        /// ���� ���������, ��� ���� �������� (HP) ������ ������ ���� ����, 
        /// ������ ������ �������� �� "dead".
        /// </summary>
        [Fact]
        public void HPDropsBelowZero()
        {
            player.HP = 1;
            var foodName = "����� ����";

            foodManager.Eat(player, foodName);

            Assert.Equal("dead", player.status);
        }

        /// <summary>
        /// ���� ���������, ��� ���� ����� dead, ��� �������� (HP) �� ���������� 
        /// ��� �������� ����� ����.
        /// </summary>
        [Fact]
        public void HPWhenPlayerIsDead()
        {
            player.status = "dead";
            var initialHP = player.HP;
            var foodName = "����";

            foodManager.Eat(player, foodName);

            Assert.Equal(initialHP, player.HP);
        }

        /// <summary>
        /// ���� ���������, ��� ������� ������ (hunger) �� ���������
        /// ������������� �������� ����� �������� ����.
        /// </summary>
        [Fact]
        public void HungerNotAbove100()
        {
            player.hunger = 95;
            var foodName = "����� ����";

            foodManager.Eat(player, foodName);

            Assert.Equal(100, player.hunger);
        }

        /// <summary>
        /// ���� ���������, ��� ������� ������ (hunger) �� ���������� ���� 0
        /// ����� �������� ����.
        /// </summary>
        [Fact]
        public void HungerNotBelow0()
        {
            player.hunger = 5;
            var foodName = "���� ����";

            foodManager.Eat(player, foodName);

            Assert.Equal(0, player.hunger);
        }
    }
}