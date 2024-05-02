using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class Player
    {
        public int _level;
        public int _exp;
        public string _name; 
        public float _attack;
        public int _defence;
        public int _maxhp;
        public int _currenthp;
        public int _maxmp;
        public int _currentmp;

        public Playerjobs _job;

        public int _gold;
        
        public List<Item> _inventory;
        public List<Quest> _quest;

        public int[] _needlevelexp;
        public Dictionary<ItemType,Item> equipItem;

        public int HP
        {
            get { return _currenthp; }
            set { _currenthp -= value; }
        }
        public int MP
        {
            get
            {
                return _currentmp;
            }
            set
            {
                _currenthp = value;
            }
        }
        public Player(int Level, string Name, int Gold)
        {
            _level = Level;
            _exp = 0;
            _name = Name;
            _attack = 15;
            _gold = Gold;
            _inventory = new List<Item>();
            _needlevelexp = [10, 25, 55, 100, 155, 225, 310, 410, 525];
            equipItem = new Dictionary<ItemType,Item>();

            //jobs 클래스 > 
            InitEquip();
        }
        private void InitEquip()
        {
            equipItem.Add(ItemType.WEAPON, null);
            Item item2 = new Item();
            equipItem.Add(ItemType.HELMET, item2);
            Item item3 = new Item();
            equipItem.Add(ItemType.ARMOR, item3);
            Item item4 = new Item();
            equipItem.Add(ItemType.SHOES, item4);
        }
    }
}
