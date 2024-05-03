using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg.Scene
{
    internal class StoreScene : BaseScene
    {
        Player player;
        List<Item> itemdata;
        List<Item> filterGearItem;
        int Itempagenum;
        Item Weapon;
        Item Helmet;
        Item Armor;
        Item Shoes;
        public override void Enter()
        {
            sceneName = SceneName.StoreScene;
            player = GameManager.Instance.player;
            itemdata = DataManager.Instance.Items;

            filterGearItem = itemdata.Where(item =>
            item._itemtype == ItemType.WEAPON || item._itemtype == ItemType.HELMET ||
            item._itemtype == ItemType.ARMOR || item._itemtype == ItemType.SHOES).ToList();
            Itempagenum = filterGearItem.Count / 9;
            ViewMenu();
        }

        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Utility.PrintTextHighlights(" - ", "상점", " - ", ConsoleColor.Red);
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Utility.PrintTextHighlights("", $"{player._gold} G", "\n", ConsoleColor.Yellow);
            Console.WriteLine("1. 장비 아이템 구매");
            Console.WriteLine("2. 소비 아이템 구매");
            Console.WriteLine("3. 보유 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    BuyGearItem();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    BuyconsumableItem();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    SellStore();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.LoadPreScene();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewMenu();
                    break;
            }
        }
        private void BuyGearItem(int startPage = 0)
        {
            Utility.PrintTextHighlights(" - ", "상점 : 장비구매", " - ", ConsoleColor.Red);
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Utility.PrintTextHighlights("", $"{player._gold} G", "\n", ConsoleColor.Yellow);
            int count = filterGearItem.Count;
            int size = 9;
            Console.WriteLine(Utility.PadRightForMixedText("- 아이템 이름", 20)
                + " | " + Utility.PadRightForMixedText($"능력치", 15)
                + " | " + Utility.PadRightForMixedText($"아이템 정보", 20)
                + " | " + $"가격");
            Console.WriteLine("---------------------------------------------------------------------------");
            for (int i = startPage * 9; i < int.Min(count, startPage * 9 + size); i++)
            {
                {
                    string isBuy = string.Empty;
                    Item item = filterGearItem[i];
                    if (item._isbuy)
                        isBuy = "구매완료";
                    else
                        isBuy = item._price.ToString();

                    Console.WriteLine(Utility.PadRightForMixedText($"- {i + 1 - startPage * 9}  {item._name}", 20)
                        + " | " + Utility.PadRightForMixedText($"{item.StatType} +{item._statvalue}", 15)
                        + " | " + Utility.PadRightForMixedText($"{item._description}", 20)
                        + " | " + $"{isBuy}");
                }
            }

            if (Itempagenum >= 1)
            {
                Console.WriteLine("\nA. 이전 페이지");
                Console.WriteLine("D. 다음 페이지");
            }
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요");

            var key = Console.ReadKey(true).Key;
            //페이지 넘기기
            if (key == ConsoleKey.A)
            {
                if (startPage == 0)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    BuyGearItem(startPage);
                }
                else
                {
                    Console.Clear();
                    BuyGearItem(--startPage);
                }
            }
            else if (key == ConsoleKey.D)
            {
                if (startPage == Itempagenum)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    BuyGearItem(startPage);
                }
                else
                {
                    Console.Clear();
                    BuyGearItem(++startPage);
                }
            }
            //구매 코드
            else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                ViewMenu();
            }
            else if ((key > ConsoleKey.D0 && key <= (ConsoleKey.D0 + int.Min(count - startPage * 9, size))))
            {
                BuyItem((int)(key - 49) + startPage * 9);
                BuyGearItem(startPage);
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + int.Min(count - startPage * 9, size))))
            {
                BuyItem((int)(key - 97) + startPage * 9);
                BuyGearItem(startPage);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                BuyGearItem(startPage);
            }
        }
        private void BuyconsumableItem()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player._gold + " G ");

            List<Item> filterItem = itemdata.Where(item => item._itemtype == ItemType.POTION).ToList();
            int cnt = 1;
            foreach (Item item in filterItem)
            {
                string isBuy = string.Empty;
                Console.WriteLine($"-{cnt}. {item._name}     | {item._itemtype} + {item._statvalue}  | {item._description} | {item._price}");
                cnt++;
            }

            Console.WriteLine("\n0. 나가기");
            //구매코드
            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key < ConsoleKey.D1 + filterItem.Count)
            {
                Console.Clear();
                BuyconsumableItem();
            }
            else if (key >= ConsoleKey.NumPad1 && key < ConsoleKey.NumPad1 + filterItem.Count)
            {
                Console.Clear();
                BuyconsumableItem();
            }
            else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                ViewMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                BuyconsumableItem();
            }

        }
        private void BuyItem(int idx)
        {
            Console.Clear();
            Item temp = itemdata[idx];
            if (temp._isbuy)
            {
                Console.Clear();
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else
            {
                if (player._gold >= temp._price)
                {
                    player._gold -= temp._price;
                    temp._isbuy = true;
                    itemdata[idx] = temp;
                    player._inventory.Add(temp);
                    Console.Clear();
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
        }
        private void SellStore()
        {
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player._gold + " G ");
            List<Item> inventory = player._inventory;
            int cnt = 1;
            foreach (Item item in inventory)
            {
                string equip = string.Empty;
                if (item.Equals(Weapon) || item.Equals(Helmet) || item.Equals(Armor) || item.Equals(Shoes))
                    equip = "[E]";
                Console.WriteLine($"- {cnt} {equip}{item._name}  | {item._itemtype} +{item._statvalue}  | {item._description}  | {item._price * 0.85f}");
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
            else if ((key > ConsoleKey.D0 && key <= (ConsoleKey.D0 + itemdata.Count)))
            {
                Console.Clear();
                SellItem((int)(key - 49));
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + itemdata.Count)))
            {
                Console.Clear();
                SellItem((int)key - 97);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                SellStore();
            }
        }
        private void SellItem(int idx)
        {
            Console.Clear();
            string isEquip = string.Empty;
            Item temp = itemdata[idx];
            if (Weapon.Equals(temp))
            {
                Weapon = new Item();
                isEquip = "착용한";
            }
            if (Helmet.Equals(temp))
            {
                Helmet = new Item();
                isEquip = "착용한";
            }
            if (Armor.Equals(temp))
            {
                Armor = new Item();
                isEquip = "착용한";
            }
            if (Shoes.Equals(temp))
            {
                Shoes = new Item();
                isEquip = "착용한";
            }
            temp._isbuy = false;
            itemdata[idx] = temp;

            player._gold += (int)Math.Round(temp._price * 0.85f);
            Console.WriteLine($"{isEquip} {temp._name}을 판매해 {temp._price * 0.85f}G를 획득했습니다.\n");
            SellStore();
        }
    }
}
