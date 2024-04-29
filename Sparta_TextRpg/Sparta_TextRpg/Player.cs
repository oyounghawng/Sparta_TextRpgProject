﻿using System;
using System.Collections.Generic;
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
    internal class Player
    {
        public int _level;
        public int _exp;
        public string _name;
        public PlayerJob _job;
        public float _attack;
        public int _defence;
        public int _Maxhp;
        public int _currnthp;
        public int _gold;

        public List<Item> _inventory;
        public int[] _needlevelexp;
        public Item _weapon;
        public Item _armor;
        public Player(int Level, string Name, PlayerJob Job, int Attack, int Defence, int Hp, int Gold)
        {
            _level = Level;
            _exp = 0;
            _name = Name;
            _job = Job;
            _attack = Attack;
            _defence = Defence;
            _Maxhp = Hp;
            _currnthp = Hp;
            _gold = Gold;
            _inventory = new List<Item>();
            _needlevelexp = [1, 2, 3, 4];
        }
        public void AddInventory(Item item)
        {
            _inventory.Add(item);
        }
    }
}
