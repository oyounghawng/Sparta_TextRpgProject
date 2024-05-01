using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PlayerJob
{
    전사,
    마법사,
    궁수,
}



namespace Sparta_TextRpg
{
    internal class Playerjobs
    {

        public PlayerJob _job;
        public float _attack;
        public int _defence;
        public int _maxhp;
        public int _maxmp;

        public List<Item> _inventory;
        public int[] _needlevelexp;
        public Item _weapon;
        public Item _armor;

        public Playerjobs( PlayerJob Job, int Attack, int Defence, int Hp, int Mp)
        {
                   
            _job = Job;
            _attack = Attack;
            _defence = Defence;
            _maxhp = Hp;
            _maxmp = Mp;

 

        }

        internal static class PlayerJobList
        {
            public static Playerjobs Warrior { get; private set; }
            public static Playerjobs Magician { get; private set; }
            public static Playerjobs Archer { get; private set; }

            static PlayerJobList()
            {
                Warrior = new Playerjobs(PlayerJob.전사, 10, 5, 150, 50);
                Magician = new Playerjobs(PlayerJob.마법사, 10, 5, 100, 100);
                Archer = new Playerjobs(PlayerJob.궁수, 15, 5, 100, 50);
            }

        }
    }
}
