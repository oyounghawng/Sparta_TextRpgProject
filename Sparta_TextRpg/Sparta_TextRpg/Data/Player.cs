using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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
        public int _gold;
        public int[] _needlevelexp;
        public Playerjobs _playerjobs;

        public List<Item> _inventory;
        public List<Quest> _quest;
        public Dictionary<ItemType, Item> equipItem;

        public Player()
        {
            _level = 1;
            _exp = 0;
            _gold = 1500;
            _inventory = new List<Item>();
            _needlevelexp = [10, 25, 55, 100, 155, 225, 310, 410, 525];
            equipItem = new Dictionary<ItemType, Item>();
            _playerjobs = new Playerjobs();
            InitEquip();
        }
        public void SetJobStat(Playerjobs playerjob)
        {
            _playerjobs = playerjob;
            _attack = playerjob._attack;
            _defence = playerjob._defence;
            _maxhp = playerjob._maxhp;
            _maxmp = playerjob._maxmp;

            _currenthp = _maxhp;
            _currentmp = _maxmp;
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
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}
