using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sparta_TextRpg.Scene
{

    internal class LoginScene : BaseScene
    {
        public override void Enter()
        {
            sceneName = SceneName.LoginScene;

            while (true)
            {
                Console.Clear();
                Utility.ShowTite();
                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
                Thread.Sleep(100);
            }
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                default:
                    Console.Clear();
                    ViewMenu();
                    break;
            }
        }

        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Console.WriteLine("딸각무한던전에 오신 여러분 환영합니다.");
            Console.WriteLine("모험을 떠날 당신의 이름은?");
            Console.Write(">>");
            string playerName = Console.ReadLine();
            GameManager.Instance.player.Name = playerName;
            GameManager.Instance.ChangeScene(SceneName.SelectCharScene);
        }
    }
}
