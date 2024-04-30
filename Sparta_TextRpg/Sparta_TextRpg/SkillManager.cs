using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sparta_TextRpg
{
    internal class SkillManager
    {


        public void Skill(Player player, Enemy enemy)
        {
            int sAttack = (int)(player._attack * (1.5 *(1.1 * 0.8)));
            enemy.hp -= sAttack;

            Console.WriteLine($"{enemy.name}에게 {sAttack}만큼의 피해를 입혔습니다.");
        }


        public void Attack(Player player, Enemy enemy)
        {
            enemy.hp -= (int)player._attack;
        }


    }
}
