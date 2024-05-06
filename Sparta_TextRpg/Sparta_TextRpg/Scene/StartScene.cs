using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class StartScene : BaseScene
    {
        public override void Enter()
        {
            sceneName = SceneName.StartScene;
            ViewMenu();
        }

        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Utility.ShowStartLogo();
            Console.WriteLine("딸깍 마을에 오신 여러분 환영합니다");
            Console.WriteLine("딸각으로 다양한 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("5. 던전입장");
            Console.WriteLine("6. 휴식하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.StatusScene);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.BattleScene);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.StoreScene);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.QuestScene);
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.DungeonScene);
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.RestScene);
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
