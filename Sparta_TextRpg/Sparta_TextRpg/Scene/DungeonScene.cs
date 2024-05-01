using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg.Scene
{
    internal class DungeonScene : BaseScene
    {
        private Player player;
        public override void Enter()
        {
            sceneName = SceneName.DungeonScene;
            GameManager.Instance.player = player;
            ViewMenu();
        }

        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1.쉬운 던전   | 방어력 5 이상 권장");
            Console.WriteLine("2.일반 던전   | 방어력 11 이상 권장");
            Console.WriteLine("3.어려운 던전 | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해 주세요.");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                    ViewMenu();
                    break;
                case ConsoleKey.NumPad1:
                case ConsoleKey.NumPad2:
                case ConsoleKey.NumPad3:
                    ViewMenu();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.LoadPreScene();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewMenu();
                    break;
            }

        }
        /*
        private void DoDungeon()
        {
            Console.Clear();
            Random random = new Random();
            int[] difficulty = { 5, 11, 17 };
            int[] reward = { 1000, 1700, 2500 };
            int curgold = player._gold;
            int curHp = player._currenthp;
            int offset = difficulty[level] - player._defence;
            int hpCost = random.Next(20 + offset, 36 + offset);
            string[] DungeonString = { "쉬운", "일반", "어려운" };
            if (player._defence < difficulty[level])
            {
                int probablity = random.Next(0, 101);

                if (probablity <= 40)
                {
                    Fail();
                }
                else
                {
                    Clear();
                }

            }
            else
            {
                Clear();
            }
            if (player._currnthp <= 0)
            {
                Console.WriteLine("사망 게임오버");
                return;
            }
            if (player._exp == player._needlevelexp[player._level - 1])
            {
                levelup();
            }
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해 주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    ViewMenu();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    DoDungeon();
                    break;
            }
        }

        private void Clear()
        {
            int rewardGold = (int)Math.Round(reward[level] * (1 + (player._attack * 2 * 0.01f)));
            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{DungeonString[level]} 던전을 클리어 하였습니다.\n");
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {curHp} -> {curHp - hpCost}");
            player._currnthp -= hpCost;
            Console.WriteLine($"골드 {curgold} -> {curgold + rewardGold}");
            player._gold += rewardGold;
            player._exp++;
        }
        private void Fail()
        {
            hpCost /= 2;
            Console.WriteLine("던전 클리어 실패");
            Console.WriteLine($"{DungeonString[level]} 던전을 클리어에 실패 하였습니다.\n");
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {curHp} -> {curHp - hpCost}");
            player._currnthp -= hpCost;
            Console.WriteLine($"골드 {curgold} -> {curgold}");
        }
        private void levelup()
        {
            player._attack += 0.5f;
            player._defence += 1;
            player._exp = 0;
            player._level += 1;
            Console.Write("\nLevel UP\n");
        }
        */
    }
        
}
