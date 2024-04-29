using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class Enemy
    {
        public string name;
        public int hp = 100;
        public int atk = 10;
        public int def = 5;
        public Enemy()
        {
            name = "슬라임";
            hp = 100;
            atk = 10;
            def = 5;
        }
    }
}
