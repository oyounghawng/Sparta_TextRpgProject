using Sparta_TextRpg.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class SelectCharScene : BaseScene
    {
        Playerjobs playerjobs;
        public override void Enter()
        {
            sceneName = SceneName.SelectCharScene;
            playerjobs = GameManager.Instance.player._playerjobs;
            ViewMenu();
        }

        public override void Excute()
        {



        }
        public override void ViewMenu()
        {
            Console.WriteLine("당신의 직업은 무엇입니까?\n");
            Playerjobs[] PlayerJobs =
            {
                playerjobs.playerjoblist.Warrior,
                playerjobs.playerjoblist.Magician,
                playerjobs.playerjoblist.Archer
            };
            for (int i = 0; i < PlayerJobs.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {PlayerJobs[i]._playerjob.ToString()}");
                Console.WriteLine($"공격력: {PlayerJobs[i]._attack}");
                Console.WriteLine($"방어력: {PlayerJobs[i]._defence}");
                Console.WriteLine($"체력: {PlayerJobs[i]._maxhp}");
                Console.WriteLine($"마나: {PlayerJobs[i]._maxmp}\n");
            }
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    GameManager.Instance.player.SetJobStat(playerjobs.playerjoblist.Warrior);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    GameManager.Instance.player.SetJobStat(playerjobs.playerjoblist.Magician);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    GameManager.Instance.player.SetJobStat(playerjobs.playerjoblist.Archer);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewMenu();
                    break;
            }
            Console.Clear();
            GameManager.Instance.ChangeScene(SceneName.StartScene);
        }
    }   
}



