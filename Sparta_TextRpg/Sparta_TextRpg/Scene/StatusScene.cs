using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class StatusScene : BaseScene
    {
        public override void Enter()
        {
            sceneName = SceneName.StatusScene;
            ViewMenu();
        }
        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            string offset = string.Empty;
            Player _player = GameManager.Instance.player;
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine("이름 : "+_player._name);
            Console.WriteLine("Lv. " + _player._level.ToString("D2"));
            Console.WriteLine($"Chad.( {_player._job})");
            Console.WriteLine("공격력. " + _player._attack + offset);  
            Console.WriteLine("방어력 " + _player._defence + offset);
            Console.WriteLine("체력 " + _player._currenthp);
            Console.WriteLine("골드 " + _player._gold + "\n");
            Console.WriteLine("1. 인벤토리\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해 주세요");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.InventoryScene);
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

    }
}