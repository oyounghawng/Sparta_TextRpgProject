using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class Quest
    {
        public string title;
        public string description;
        public string goal;
        public Enemy enemy;
        public int gold;
        public int curcnt;
        public int goalcnt;

        public Quest(string _title, string _description, string _goal, int _gold, int _goalcnt, Enemy _enemy)
        {
            title = "마을을 위협하는 슬라임 처치";
            description = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n" +
                          "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                          "모험가인 자네가 좀 처치해주게!";
            goal = "스켈레톤 5 마리 처치";
            gold = 5;
            curcnt = 0;
            goalcnt = 1;
            enemy = _enemy;
        }
        public void cntQuest(Enemy _enemy)
        {
            if(enemy.name == _enemy.name)
            {
                curcnt++;
            }
        }
    }
}
