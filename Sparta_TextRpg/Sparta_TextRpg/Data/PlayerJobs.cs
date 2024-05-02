using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparta_TextRpg.Playerjobs;

enum Playerjob
{
    전사,
    궁수,
    마법사
}

namespace Sparta_TextRpg
{
    internal class Playerjobs
    {
        public Playerjob _playerjob;
        public float _attack;
        public int _defence;
        public int _maxhp;
        public int _maxmp;

        public string Skill1_Name;
        public string Skill2_Name;

        public PlayerJobList playerjoblist;

        public Playerjobs()
        {
            playerjoblist = new PlayerJobList();
        }
        public Playerjobs Setjob(Playerjob playerjob)
        {
            switch (playerjob)
            {
                case Playerjob.전사:
                    return playerjoblist.Warrior;
                case Playerjob.마법사:
                    return playerjoblist.Warrior;
                case Playerjob.궁수:
                    return playerjoblist.Warrior;
                default:
                    return new Playerjobs();
            }
        }
        private Playerjobs(Playerjob playerjob, int attack, int defence, int hp, int mp, string skill1, string skill2)
        {
            _playerjob = playerjob;
            _attack = attack;
            _defence = defence;
            _maxhp = hp;
            _maxmp = mp;
            Skill1_Name = skill1;
            Skill2_Name = skill2;
        }

        //>> LIST
        public class PlayerJobList
        {
            public Playerjobs Warrior { get; private set; }
            public Playerjobs Magician { get; private set; }
            public Playerjobs Archer { get; private set; }

            public PlayerJobList()
            {
                Warrior = new Playerjobs(Playerjob.전사, 10, 5, 150, 50, "베쉬", "볼링베쉬");
                Magician = new Playerjobs(Playerjob.마법사, 10, 5, 100, 100, "파이어볼", "메테오");
                Archer = new Playerjobs(Playerjob.궁수, 10, 5, 100, 50, "집중사격", "멀티에로우");
            }
        }
    }
}
