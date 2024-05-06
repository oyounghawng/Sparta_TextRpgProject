using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg.Scene
{
    internal class RestScene : BaseScene
    {
        private Player player;
        public override void Enter()
        {
            sceneName = SceneName.RestScene;
            player = GameManager.Instance.player;
            player.HP = 30;
            player.MP = 20;
            ViewMenu();
        }

        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            Utility.PrintTextHighlights("- ", "휴식하기", " - \n", ConsoleColor.Red);
            Console.WriteLine($"500 G 를 내면 체력과 마나를 회복할 수 있습니다. (보유 골드 : {player._gold} G)");
            ShowPlayerStat();
            Console.WriteLine("\n1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해 주세요.");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    TakeRest();
                    ViewMenu();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.StartScene);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewMenu();
                    break;
            }
        }
        private void ShowPlayerStat()
        {
            Console.WriteLine(Utility.PadRightForMixedText("이름", 13) + " : " + player._name);
            Console.WriteLine(Utility.PadRightForMixedText("Lv", 13) + " : " + player._level.ToString("D2"));
            Console.WriteLine(Utility.PadRightForMixedText("Chad", 13) + " : " + player._playerjobs._playerjob);
            Console.WriteLine(Utility.PadRightForMixedText("체력", 13) + " : " + $"{player._currenthp} / {player._maxhp}");
            Console.WriteLine(Utility.PadRightForMixedText("마나", 13) + " : " + $"{player._currentmp} / {player._maxmp}");
        }
        private void TakeRest()
        {
            if (player._gold < 500)
            {
                Utility.PrintTextHighlights(" ", "골드가 부족합니다.", "", ConsoleColor.Red);
                ViewMenu();
            }
            if(player.HP == player._maxhp && player.MP == player._maxmp)
            {
                Utility.PrintTextHighlights(" ", "체력과 마나가 최대입니다.", "", ConsoleColor.Red);
                ViewMenu();
            }
            Utility.PrintTextHighlights("", "휴식을 완료했습니다.", "", ConsoleColor.Magenta);
            Utility.PrintTextHighlights("체력을 ", $"{player._maxhp - player._currenthp}", " 회복했습니다.", ConsoleColor.Green);
            player.HealHP = player._maxhp;
            Utility.PrintTextHighlights("마나를 ", $"{player._maxmp - player._currentmp}", " 회복했습니다.\n", ConsoleColor.Green);
            player.HealMP = player._maxmp;
            ShowPlayerStat();
            Console.WriteLine("\n0. 다음");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    ViewMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewMenu();
                    break;
            }
        }
    }
}
