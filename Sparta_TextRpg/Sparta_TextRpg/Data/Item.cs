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
    SHOES
}
namespace Sparta_TextRpg
{
    internal class Item
    {
        public string _name;
        public ItemType _itemtype;
        public int _statvalue;
        public string _description;

        public bool _isequip;
        public bool _isbuy;
        public int _price;

        public Item()
        {

        }
        public Item(string name, ItemType type, int statvalue, string description, int price, bool isequip = false, bool isbuy = false)
        {
            _name = name;
            _itemtype = type;
            _statvalue = statvalue;
            _description = description;
            _isequip = isequip;
            _price = price;
        }

    }
}
