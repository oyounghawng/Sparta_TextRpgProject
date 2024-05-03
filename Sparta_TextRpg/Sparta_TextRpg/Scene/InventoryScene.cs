using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class InventoryScene : BaseScene
    {
        private List<Item> inventory;
        private Player player;
        Item Weapon;
        Item Helmet;
        Item Armor;
        Item Shoes;
        public override void Enter()
        {
            sceneName = SceneName.InventoryScene;
            inventory = GameManager.Instance.player._inventory;
            player = GameManager.Instance.player;
            ViewMenu();
        }
        public override void Excute()
        {
        }
        public override void ViewMenu()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템목록]");
            CheckEquipItem();
            foreach (Item item in inventory)
            {
                string equip = string.Empty;
                if (item.Equals(Weapon) || item.Equals(Helmet) || item.Equals(Armor) || item.Equals(Shoes))
                    equip = "[E]";
                Console.WriteLine($"- {equip}{item._name}     | {item._itemtype} +{item._statvalue}  | {item._description}");
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    EquipInventory();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.StatusScene);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewMenu();
                    break;
            }
        }
        private void EquipInventory()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템목록]");

            CheckEquipItem();

            int cnt = 1;
            foreach (Item item in inventory)
            {
                string equip = string.Empty;
                if (item.Equals(Weapon) || item.Equals(Helmet) || item.Equals(Armor) || item.Equals(Shoes))
                    equip = "[E]";
                Console.WriteLine($"-{cnt} {equip}{item._name}     | {item._itemtype} +{item._statvalue}  | {item._description}");
                cnt++;
            }
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                ViewMenu();
            }
            else if (key > ConsoleKey.D0 && key <= (ConsoleKey.D0 + inventory.Count))
            {
                Console.Clear();
                Equip((int)(key - 49));
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + inventory.Count)))
            {
                Console.Clear();
                Equip((int)(key - 97));
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                EquipInventory();
            }
        }
        private void Equip(int idx)
        {
            Item temp = inventory[idx];
            CheckEquipItem();
            switch (temp._itemtype)
            {
                case ItemType.WEAPON:
                    EquipItem(ItemType.WEAPON, temp);
                    break;
                case ItemType.HELMET:
                    EquipItem(ItemType.HELMET, temp);
                    break;
                case ItemType.ARMOR:
                    EquipItem(ItemType.ARMOR, temp);
                    break;
                case ItemType.SHOES:
                    EquipItem(ItemType.SHOES, temp);
                    break;
                default:
                    Console.WriteLine("해당되는 장착타입이 없습니다.");
                    break;
            }
            EquipInventory();
        }
        private void CheckEquipItem()
        {
            if (player.equipItem.ContainsKey(ItemType.WEAPON))
                Weapon = player.equipItem[ItemType.WEAPON];
            else
                Weapon = null;
            if (player.equipItem.ContainsKey(ItemType.HELMET))
                Helmet = player.equipItem[ItemType.HELMET];
            else
                Helmet = null;
            if (player.equipItem.ContainsKey(ItemType.ARMOR))
                Armor = player.equipItem[ItemType.ARMOR];
            else
                Armor = null;
            if (player.equipItem.ContainsKey(ItemType.SHOES))
                Shoes = player.equipItem[ItemType.SHOES];
            else
                Shoes = null;
        }
        private void EquipItem(ItemType type, Item item)
        {
            if (!player.equipItem.ContainsKey(type))
            {
                player.EquipItem(type, item);
            }
            else
            {
                Console.Clear();
                if (Weapon.Equals(item))
                {
                    Console.WriteLine("이미 장착중인 무기입니다.");
                }
                else
                {
                    player.EquipItem(type, item);
                }
            }
        }
    }
}
