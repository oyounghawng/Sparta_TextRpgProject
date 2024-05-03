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
        public Enemy(string _name = "")
        {
            name = _name;
            level = 1;
            hp = 100;
            atk = 10;
            def = 5;
            exp = 5;
            isDie = false;
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
            enemy.hp = 100;
            enemy.atk = 10;
            enemy.def = 5;
            enemy.exp = 5;
            enemy.isDie = false;
            return enemy;
        }
        public string PrintEnemy(Enemy enemy)
        {
            string Diestring = !enemy.isDie ? enemy.hp.ToString() : "Dead";
            return $"Lv.{enemy.level} {enemy.name}   HP  {Diestring}";
        }
    }
}
