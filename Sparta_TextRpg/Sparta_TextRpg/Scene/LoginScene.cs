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
            ViewMenu();
        }

        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");
            string playerName = Console.ReadLine();
            GameManager.Instance.player._name = playerName;
            GameManager.Instance.ChangeScene(SceneName.SelectCharScene);
        }
    }
}
