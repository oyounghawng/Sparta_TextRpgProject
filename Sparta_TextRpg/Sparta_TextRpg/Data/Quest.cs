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
            title = _title;
            description = _description;
            goal = _goal;
            gold = _gold;
            curcnt = 0;
            goalcnt = _goalcnt;
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
