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

        List<Item> filterGearItem;
        List<Item> filterConsumableItem;

        Item Weapon;
        Item Helmet;
        Item Armor;
        Item Shoes;

        private int Itempagenum;
        public override void Enter()
        {
            sceneName = SceneName.InventoryScene;
            inventory = GameManager.Instance.player._inventory;
            player = GameManager.Instance.player;

            filterGearItem = inventory.Where(item =>
                item._itemtype == ItemType.WEAPON || item._itemtype == ItemType.HELMET ||
                item._itemtype == ItemType.ARMOR || item._itemtype == ItemType.SHOES).ToList();

            filterConsumableItem = inventory.Where(item => item._itemtype == ItemType.POTION).ToList();
            Itempagenum = filterGearItem.Count / 9;

            ViewMenu();
        }
        public override void Excute()
        {
        }
        public override void ViewMenu()
        {
            Utility.PrintTextHighlights(" - ", "인벤토리", " - ", ConsoleColor.Red);
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Utility.PrintTextHighlights("", "[장비아이템 목록]", "", ConsoleColor.Green);
            Console.WriteLine(Utility.PadRightForMixedText("  아이템 이름", 20)
    + " | " + Utility.PadRightForMixedText($"능력치", 15)
    + " | " + Utility.PadRightForMixedText($"아이템 정보", 20));
            Console.WriteLine("------------------------------------------------------------------------------------------");

            CheckEquipItem();
            foreach (Item item in filterGearItem)
            {
                string equip = string.Empty;
                if (item.Equals(Weapon) || item.Equals(Helmet) || item.Equals(Armor) || item.Equals(Shoes))
                    equip = "[E]";

                Console.WriteLine(Utility.PadRightForMixedText($"- {equip}{item._name}", 20)
                + " | " + Utility.PadRightForMixedText($"{item.StatType} +{item._statvalue}", 15)
                + " | " + Utility.PadRightForMixedText($"{item._description}", 20));
            }

            Utility.PrintTextHighlights("\n", "[소비아이템 목록]", "", ConsoleColor.Cyan);
            Console.WriteLine(Utility.PadRightForMixedText("  아이템 이름", 20)
    + " | " + Utility.PadRightForMixedText($"능력치", 15)
    + " | " + Utility.PadRightForMixedText($"아이템 정보", 20)
    + " | " + "보유수량");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            foreach (Item item in filterConsumableItem)
            {
                Console.WriteLine(Utility.PadRightForMixedText($"- {item._name}", 20)
                + " | " + Utility.PadRightForMixedText($"{item.StatType} +{item._statvalue}", 15)
                + " | " + Utility.PadRightForMixedText($"{item._description}", 20)
                + " | " + item._cnt);
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
        private void EquipInventory(int startPage = 0)
        {
            Utility.PrintTextHighlights(" - ", "인벤토리 : 장착관리", " - ", ConsoleColor.Red);
            Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.\n");
            Utility.PrintTextHighlights("", "[장비아이템 목록]", "", ConsoleColor.Green);
            CheckEquipItem();

            int count = filterGearItem.Count;
            int size = 9;

            int cnt = 1;
            for (int i = startPage * 9; i < int.Min(count, startPage * 9 + size); i++)
            {
                string equip = string.Empty;
                Item item = filterGearItem[i];
                if (item.Equals(Weapon) || item.Equals(Helmet) || item.Equals(Armor) || item.Equals(Shoes))
                    equip = "[E]";

                Console.WriteLine(Utility.PadRightForMixedText($"-{cnt} {equip}{item._name}", 20)
                + " | " + Utility.PadRightForMixedText($"{item.StatType} +{item._statvalue}", 15)
                + " | " + Utility.PadRightForMixedText($"{item._description}", 20));
                cnt++;
            }

            if (Itempagenum >= 1)
            {
                Console.WriteLine("\nA. 이전 페이지");
                Console.WriteLine("D. 다음 페이지");
            }

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.A)
            {
                if (startPage == 0)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    EquipInventory(startPage);
                }
                else
                {
                    Console.Clear();
                    EquipInventory(--startPage);
                }
            }
            else if (key == ConsoleKey.D)
            {
                if (startPage == Itempagenum)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    EquipInventory(startPage);
                }
                else
                {
                    Console.Clear();
                    EquipInventory(++startPage);
                }
            }
            else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                ViewMenu();
            }
            else if (key > ConsoleKey.D0 && key <= (ConsoleKey.D0 + int.Min(count - startPage * 9, size)))
            {
                Console.Clear();
                Equip((int)(key - 49) + startPage * 9);
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + int.Min(count - startPage * 9, size))))
            {
                Console.Clear();
                Equip((int)(key - 97) + startPage * 9);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                EquipInventory();
            }
        }
        private void Equip(int idx)
        {
            Item temp = filterGearItem[idx];
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
            if (player._equipItem.ContainsKey(ItemType.WEAPON))
                Weapon = player._equipItem[ItemType.WEAPON];
            else
                Weapon = null;
            if (player._equipItem.ContainsKey(ItemType.HELMET))
                Helmet = player._equipItem[ItemType.HELMET];
            else
                Helmet = null;
            if (player._equipItem.ContainsKey(ItemType.ARMOR))
                Armor = player._equipItem[ItemType.ARMOR];
            else
                Armor = null;
            if (player._equipItem.ContainsKey(ItemType.SHOES))
                Shoes = player._equipItem[ItemType.SHOES];
            else
                Shoes = null;
        }
        private void EquipItem(ItemType type, Item item)
        {
            if (!player._equipItem.ContainsKey(type))
            {
                player.EquipItem(type, item);
            }
            else
            {
                Console.Clear();
                if (item.Equals(Weapon) || item.Equals(Helmet)|| item.Equals(Armor)|| item.Equals(Shoes))
                {
                    Console.WriteLine("이미 장착중인 장비입니다.");
                }
                else
                {
                    player.EquipItem(type, item);
                }
            }
        }
    }
}
