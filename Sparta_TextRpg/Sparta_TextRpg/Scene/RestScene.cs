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
        public override void Enter()
        {
            sceneName = SceneName.RestScene;
            ViewMenu();
        }

        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : G)");
            Console.WriteLine($"현재 체력 : \n");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해 주세요.");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
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
        private void TakeRest()
        {
            Console.WriteLine("휴식을 완료했습니다.\n");
        }
    }
}
