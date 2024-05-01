using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PlayerJobs
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
        public int _maxhp;
        public int _currenthp;
        public int _maxmp;
        public int _currentmp;

        public int _gold;
        
        public List<Item> _inventory;
        public int[] _needlevelexp;
        public Dictionary<ItemType,Item> equipItem;

        public Player(int Level, string Name, PlayerJob Job, int Attack, int Defence, int Hp, int Gold, int _exp,int Mp)
        {
            _level = Level;
            _exp = 0;
            _name = Name;
            _job = Job;
            _attack = Attack;
            _defence = Defence;
            _maxhp = Hp;
            _currenthp = Hp;
            _maxmp = Mp;
            _currentmp = Mp;
            _gold = Gold;
            _inventory = new List<Item>();
            _needlevelexp = [1, 2, 3, 4];
            equipItem = new Dictionary<ItemType,Item>();

            AddEquip();
        }
        private void AddEquip()
        {
            Item item1 = new Item();
            equipItem.Add(ItemType.WEAPON, item1);
            Item item2 = new Item();
            equipItem.Add(ItemType.HELMET, item2);
            Item item3 = new Item();
            equipItem.Add(ItemType.ARMOR, item3);
            Item item4 = new Item();
            equipItem.Add(ItemType.SHOES, item4);
        }
        public int HP
        {
            get { return _currenthp; }
            set { _currenthp -= value; }
        }
        public void AddInventory(Item item)
        {
            equipItem[item._itemtype] = item;
        }
    }
}
