using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class Enemy
    {
        public string name;
        public int level;
        public int hp;
        public int atk;
        public int def;
        public int exp;

        public bool isDie = false;

        public Enemy()
        {

        }
        public Enemy(string _name, int _hp, int _atk, int _def)
        {
            name = _name;
            level = 1;
            hp = _hp;
            atk = _atk;
            def = _def;
            exp = 5;
            isDie = false;
        }
        public void SetLevelStat(int _level)
        {
            level = _level;
            _level--;
            hp += 5 * _level;
            atk += _level * 2;
            def += _level;
            exp += _level * 3;
        }
        public int HP
        {
            get { return hp; }
            set
            {
                hp -= value;
                if (hp <= 0)
                {
                    isDie = true;
                    hp = 0;
                }
            }
        }

        public Enemy DeepCopy(Enemy _enemy)
        {
            Enemy enemy = new Enemy();
            enemy.name = _enemy.name;
            enemy.hp = _enemy.hp;
            enemy.atk = _enemy.atk;
            enemy.def = _enemy.def;
            enemy.exp = _enemy.exp;
            enemy.isDie = false;
            return enemy;
        }
    }
}
