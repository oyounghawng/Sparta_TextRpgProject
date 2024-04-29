using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class BaseScene
    {
        public SceneName sceneName = SceneName.Null;

        public virtual void Enter()
        {
            // 장면에 진입할 때 호출
        }

        public virtual void Excute()
        {

            // 장면이 활성화 상태일 때 주기적 호출

        }
        public virtual void ViewMenu()
        {
            // 장면을 보여주는 메뉴
        }
    }
}
