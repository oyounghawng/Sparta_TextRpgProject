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
        Playerjobs[] playerjobsList;
        public override void Enter()
        {
            sceneName = SceneName.SelectCharScene;
            playerjobs = GameManager.Instance.player._playerjobs;
            playerjobsList = [playerjobs.playerjoblist.Warrior, playerjobs.playerjoblist.Magician, playerjobs.playerjoblist.Archer];
            ViewMenu();
        }

        public override void Excute()
        {



        }
        public override void ViewMenu()
        {
            Console.WriteLine("당신의 직업은 무엇입니까?\n");
            for (int i = 0; i < playerjobsList.Length; i++)
            {
                Utility.PrintTextHighlights("", $"{i + 1}. {playerjobsList[i]._playerjob.ToString()}", 
                    "", ConsoleColor.Red);
                Console.WriteLine($"공격력: {playerjobsList[i]._attack}");
                Console.WriteLine($"방어력: {playerjobsList[i]._defence}");
                Console.WriteLine($"체력: {playerjobsList[i]._maxhp}");
                Console.WriteLine($"마나: {playerjobsList[i]._maxmp}\n");
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



