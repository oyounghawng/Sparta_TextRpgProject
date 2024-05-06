using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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

        public int _critical;
        public int _dodge;

        private int modifierCritical;
        private int modifierDodge;
        private float modifierattck;
        private int modifierdefence;

        public List<Item> _inventory;
        public List<Quest> _quest;
        public Dictionary<ItemType, Item> _equipItem;

        public Player()
        {
            _level = 1;
            _exp = 0;
            _gold = 5000;
            _critical = 15;
            _dodge = 10;
            _inventory = new List<Item>();
            _needlevelexp = [10, 25, 55, 100, 155, 225, 310, 410, 525];
            _equipItem = new Dictionary<ItemType, Item>();
            _playerjobs = new Playerjobs();
            _quest = new List<Quest>();
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
        public void EquipItem(ItemType type, Item item)
        {
            if (!_equipItem.ContainsKey(type))
            {
                _equipItem.Add(type, item);
            }
            else
            {
                _equipItem[type] = item;
            }
            ModiferStat();
        }
        private void ModiferStat()
        {
            modifierattck = 0;
            modifierdefence = 0;
            modifierDodge = 0;
            if (_equipItem.ContainsKey(ItemType.WEAPON))
            {
                switch (_equipItem[ItemType.WEAPON]._itemrating)
                {
                    case ItemRating.RARE:
                        modifierCritical = 10;
                        break;
                    case ItemRating.UNIQUE:
                        modifierCritical = 20;
                        break;
                    case ItemRating.LEGEND:
                        modifierCritical = 30;
                        break;
                }
                modifierCritical = _equipItem[ItemType.WEAPON]._statvalue;
                modifierattck += _equipItem[ItemType.WEAPON]._statvalue;
            }
            if (_equipItem.ContainsKey(ItemType.HELMET))
            {
                switch (_equipItem[ItemType.HELMET]._itemrating)
                {
                    case ItemRating.RARE:
                        modifierDodge += 5;
                        break;
                    case ItemRating.UNIQUE:
                        modifierDodge += 10;
                        break;
                    case ItemRating.LEGEND:
                        modifierDodge += 15;
                        break;
                }
                modifierdefence += _equipItem[ItemType.HELMET]._statvalue;
            }
            if (_equipItem.ContainsKey(ItemType.ARMOR))
            {
                switch (_equipItem[ItemType.ARMOR]._itemrating)
                {
                    case ItemRating.RARE:
                        modifierDodge += 5;
                        break;
                    case ItemRating.UNIQUE:
                        modifierDodge += 10;
                        break;
                    case ItemRating.LEGEND:
                        modifierDodge += 15;
                        break;
                }
                modifierdefence += _equipItem[ItemType.ARMOR]._statvalue;
            }
            if (_equipItem.ContainsKey(ItemType.SHOES))
            {
                switch (_equipItem[ItemType.SHOES]._itemrating)
                {
                    case ItemRating.RARE:
                        modifierDodge += 5;
                        break;
                    case ItemRating.UNIQUE:
                        modifierDodge += 10;
                        break;
                    case ItemRating.LEGEND:
                        modifierDodge += 15;
                        break;
                }
                modifierdefence += _equipItem[ItemType.SHOES]._statvalue;
            }
        }
        public void LevelUP()
        {
            _attack += 0.5f; // 공격력 0.5 증가
            _defence += 1; // 방어력 1 증가
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
                _currentmp -= value;
            }
        }
        public float Attack
        {
            get
            {
                return _attack + modifierattck;
            }
            private set { }
        }
        public int Deffence
        {
            get
            {
                return _defence + modifierdefence;
            }
            private set { }
        }
        public int Critical
        {
            get
            {
                return _critical + modifierCritical;
            }
            private set { }
        }
        public int Dodge
        {
            get
            {
                return _dodge + modifierDodge;
            }
            private set { }
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
        public int HealHP
        {
            set
            {
                _currenthp += value;
                if (_currenthp > _maxhp)
                {
                    _currenthp = _maxhp;
                }
            }
        }
        public int HealMP
        {
            set
            {
                _currentmp += value;
                if (_currenthp > _maxhp)
                {
                    _currenthp = _maxhp;
                }
            }
        }

    }
}
