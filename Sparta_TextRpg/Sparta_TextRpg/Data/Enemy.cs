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
        public int level = 0;
        public int hp = 100;
        public int atk = 10;
        public int def = 5;
        public int exp = 0;

        public bool isDie = false;
        public Enemy()
        {
            name = "슬라임";
            level = 0;
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
                    isDie = true;
            }
        }
        public string PrintEnemy(Enemy enemy)
        {
            string Diestring = !enemy.isDie ? enemy.hp.ToString() : "Dead";
            return $"Lv.{enemy.level} {enemy.name}   HP  {Diestring}";
        }
    }
}
