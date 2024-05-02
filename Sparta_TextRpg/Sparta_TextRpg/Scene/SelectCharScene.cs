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


        public override void Enter()
        {
            sceneName = SceneName.SelectCharScene;
            ViewMenu();
        }

        public override void Excute()
        {



        }
        public override void ViewMenu()
        {


            Playerjobs[] PlayerJobs =
            {
                Playerjobs.PlayerJobList.Warrior,
                Playerjobs.PlayerJobList.Magician,
                Playerjobs.PlayerJobList.Archer
            };

            for (int i = 0; i < PlayerJobs.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {PlayerJobs[i]._playerjob.ToString()}");
                Console.WriteLine($"공격력: {PlayerJobs[i]._attack}");
                Console.WriteLine($"방어력: {PlayerJobs[i]._defence}");
                Console.WriteLine($"체력: {PlayerJobs[i]._maxhp}");
                Console.WriteLine($"마나: {PlayerJobs[i]._maxmp}\n");
            }

            Console.WriteLine("원하시는 캐릭터를 입력해주세요.");



            var key = Console.ReadKey(true).Key;
            Player selectedPlayer = null;

            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    selectedPlayer = CreatePlayer(PlayerJobs[0]);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    selectedPlayer = CreatePlayer(PlayerJobs[1]);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    selectedPlayer = CreatePlayer(PlayerJobs[2]);
                    break;


                default:

                    Console.WriteLine("잘못된 입력입니다.");

                    
                    break;
            }
            if (selectedPlayer != null)
            {
                Console.Clear();
                GameManager.Instance.player = selectedPlayer;
                GameManager.Instance.ChangeScene(SceneName.StartScene);
            }

        }
        private Player CreatePlayer(Playerjobs selectedJob)
        {


            string jobName = selectedJob._job;

            return new Player
                (
                1,
                "", 
                selectedJob._job,
                Convert.ToInt32(selectedJob._attack), 
                selectedJob._defence, 
                selectedJob._maxhp, 
                selectedJob._maxmp, 
                0 
                );

        }
    }
}

    
 
