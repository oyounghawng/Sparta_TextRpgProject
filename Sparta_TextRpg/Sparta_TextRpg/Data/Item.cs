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
        public Item(string name, ItemType type, ItemRating rType, int statvalue, string description, int price, bool isbuy = false)
        {
            _name = name;
            _itemtype = type;
            _itemrating = rType;
            _statvalue = statvalue;
            _description = description;
            _price = price;
            _cnt = 0;
        }
    }
}
