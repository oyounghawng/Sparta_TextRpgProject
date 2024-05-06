using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ItemType
{
    WEAPON,
    HELMET,
    ARMOR,
    SHOES,
    POTION
}
public enum ItemRating
{
    LEGEND,
    UNIQUE,
    RARE,
}

namespace Sparta_TextRpg
{
    internal class Item
    {
        public string _name;
        public ItemType _itemtype;
        public int _statvalue;
        public string _description;
        public ItemRating _itemrating;
        public bool _isbuy;
        public int _price;
        public int _cnt;
        public Item()
        {

        }
        public Item(string name, ItemType type, ItemRating rType, int statvalue, int cnt ,string description, int price, bool isbuy = false)
        {
            _name = name;
            _itemtype = type;
            _itemrating = rType;
            _statvalue = statvalue;
            _description = description;
            _price = price;
            _cnt = cnt;
        }
        public Item DeepCopy(Item _item)
        {
            Item item = new Item();
            item._name = _item._name;
            item._itemtype = _item._itemtype;
            item._statvalue = _item._statvalue;
            item._description = _item._description;
            item._itemrating = _item._itemrating;
            item._isbuy = _item._isbuy;
            item._price = _item._price;
            item._cnt = 1;
            return item;
        }

        public string StatType
        {
            get
            {
                if (_itemtype == ItemType.WEAPON)
                    return "공격력";
                else if (_itemtype == ItemType.POTION)
                {
                    if(_name.Contains("체력"))
                        return "체력 회복량";
                    else
                        return "마나 회복량";
                }
                    
                else
                    return "방어력";
            }
            private set { }
        }
    }
}
