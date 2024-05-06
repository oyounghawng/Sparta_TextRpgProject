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
        public string difficulty;
        public string reward;
        public string goal;
        public Enemy enemy;
        public int gold;
        public int curcnt;
        public int goalcnt;

        public Quest()
        {

        }
        public Quest(string _title, string _description, string _difficulty, string _reward, string _goal, int _gold, int _goalcnt, Enemy _enemy)
        {
            title = _title;
            description = _description;
            difficulty = _difficulty;
            reward = _reward;
            goal = _goal;
            gold = _gold;
            curcnt = 0;
            goalcnt = _goalcnt;
            enemy = _enemy;
        }
        public Quest DeepCopy(Quest _quest)
        {
            Quest quest = new Quest();
            quest.title =  _quest.title;
            quest.description = _quest.description;
            quest.difficulty = _quest.difficulty;
            quest.reward = _quest.reward;
            quest.goal = _quest.goal;
            quest.gold = _quest.gold;
            quest.curcnt = 0;
            quest.goalcnt = _quest.goalcnt;
            quest.enemy = _quest.enemy;

            return quest;
        }
        public void cntQuest(Enemy _enemy)
        {
            if (enemy.name == _enemy.name)
            {
                curcnt++;
            }
        }
    }
}
