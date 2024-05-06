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
        List<Item> filterConsumableItem;

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

            filterConsumableItem = itemdata.Where(item => item._itemtype == ItemType.POTION).ToList();
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
                    BuyGearView();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    BuyconsumableView();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    SellItemView();
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

        #region View
        private void BuyGearView(int startPage = 0)
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
            Console.WriteLine("------------------------------------------------------------------------------------------");
            for (int i = startPage * 9; i < int.Min(count, startPage * 9 + size); i++)
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
                    BuyGearView(startPage);
                }
                else
                {
                    Console.Clear();
                    BuyGearView(--startPage);
                }
            }
            else if (key == ConsoleKey.D)
            {
                if (startPage == Itempagenum)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    BuyGearView(startPage);
                }
                else
                {
                    Console.Clear();
                    BuyGearView(++startPage);
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
                BuyGearItem((int)(key - 49) + startPage * 9);
                BuyGearView(startPage);
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + int.Min(count - startPage * 9, size))))
            {
                BuyGearItem((int)(key - 97) + startPage * 9);
                BuyGearView(startPage);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                BuyGearItem(startPage);
            }
        }
        private void BuyconsumableView()
        {
            Utility.PrintTextHighlights(" - ", "상점 : 소비아이템 구매", " - ", ConsoleColor.Red);
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Utility.PrintTextHighlights("", $"{player._gold} G", "\n", ConsoleColor.Yellow);

            Console.WriteLine(Utility.PadRightForMixedText("- 아이템 이름", 20)
                + " | " + Utility.PadRightForMixedText($"능력치", 15)
                + " | " + Utility.PadRightForMixedText($"아이템 정보", 20)
                + " | " + Utility.PadRightForMixedText($"가격", 20)
                + " | " + "보유수량");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            int cnt = 1;
            foreach (Item filteritem in filterConsumableItem)
            {
                Item potion = player._inventory.FirstOrDefault(item => item._name == filteritem._name);
                string potioncnt = string.Empty;
                if (potion == null)
                    potioncnt = "0";
                else
                    potioncnt = potion._cnt.ToString();

                Console.WriteLine(Utility.PadRightForMixedText($"- {cnt}.  {filteritem._name}", 20)
            + " | " + Utility.PadRightForMixedText($"{filteritem.StatType} +{filteritem._statvalue}", 15)
            + " | " + Utility.PadRightForMixedText($"{filteritem._description}", 20)
            + " | " + Utility.PadRightForMixedText($"{filteritem._price}", 20)
            + " | " + potioncnt);
                cnt++;
            }
            Console.WriteLine("\n0. 나가기");
            //구매코드
            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key < ConsoleKey.D1 + filterConsumableItem.Count)
            {
                Console.Clear();
                BuyConsumableItem((int)(key - 49));
            }
            else if (key >= ConsoleKey.NumPad1 && key < ConsoleKey.NumPad1 + filterConsumableItem.Count)
            {
                Console.Clear();
                BuyConsumableItem((int)(key - 97));
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
                BuyconsumableView();
            }

        }
        private void SellItemView(int startPage = 0)
        {
            List<Item> playerinventory = player._inventory;
            Utility.PrintTextHighlights(" - ", "상점 : 장비판매", " - ", ConsoleColor.Red);
            Console.WriteLine("필요한 아이템을 판매 할 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Utility.PrintTextHighlights("", $"{player._gold} G", "\n", ConsoleColor.Yellow);

            int count = playerinventory.Count;
            int size = 9;
            Console.WriteLine(Utility.PadRightForMixedText("- 아이템 이름", 20)
                + " | " + Utility.PadRightForMixedText($"능력치", 15)
                + " | " + Utility.PadRightForMixedText($"아이템 정보", 20)
                + " | " + $"판매가격");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            CheckEquipItem();
            for (int i = startPage * 9; i < int.Min(count, startPage * 9 + size); i++)
            {
                string equip = string.Empty;
                if (playerinventory[i].Equals(Weapon) || playerinventory[i].Equals(Helmet) || playerinventory[i].Equals(Armor) || playerinventory[i].Equals(Shoes))
                    equip = "[E]";
                Item item = filterGearItem[i];

                Console.WriteLine(Utility.PadRightForMixedText($"- {i + 1 - startPage * 9} {equip}{item._name}", 20)
                    + " | " + Utility.PadRightForMixedText($"{item.StatType} +{item._statvalue}", 15)
                    + " | " + Utility.PadRightForMixedText($"{item._description}", 20)
                    + " | " + $"{(int)Math.Round(item._price * 0.85f)}");
            }
            int invenPage = playerinventory.Count;
            if (invenPage >= 1)
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
                    SellItemView(startPage);
                }
                else
                {
                    Console.Clear();
                    SellItemView(--startPage);
                }
            }
            else if (key == ConsoleKey.D)
            {
                if (startPage == Itempagenum)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    SellItemView(startPage);
                }
                else
                {
                    Console.Clear();
                    SellItemView(++startPage);
                }
            }
            //판매 코드
            else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                ViewMenu();
            }
            else if ((key > ConsoleKey.D0 && key <= (ConsoleKey.D0 + int.Min(count - startPage * 9, size))))
            {
                SellItem(playerinventory[(int)(key - 49) + startPage * 9]);
                SellItemView(startPage);
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + int.Min(count - startPage * 9, size))))
            {
                SellItem(playerinventory[(int)(key - 97) + startPage * 9]);
                SellItemView(startPage);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                SellItemView(startPage);
            }
        }
        #endregion


        private void BuyGearItem(int idx)
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
                    temp = itemdata[idx].DeepCopy(itemdata[idx]);
                    player._inventory.Add(temp);
                    Console.Clear();
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                {
                    Console.Clear();
                    Utility.PrintTextHighlights("", "골드가 부족합니다.", "", ConsoleColor.Red);
                }
            }
        }
        private void BuyConsumableItem(int idx)
        {
            List<Item> filterItem = itemdata.Where(item => item._itemtype == ItemType.POTION).ToList();
            Item temp = filterItem[idx].DeepCopy(filterItem[idx]);
            if (player._gold < temp._price)
            {
                Console.Clear();
                Utility.PrintTextHighlights("", "골드가 부족합니다.", "", ConsoleColor.Red);
                BuyconsumableView();
            }
            Console.Clear();
            Console.WriteLine("구매완료");
            Item potion = player._inventory.FirstOrDefault(item => item._name == temp._name);
            player._gold -= temp._price;
            if (potion != null)
            {
                potion._cnt += 1;
            }
            else
            {
                player._inventory.Add(temp);
            }
            BuyconsumableView();
        }
        private void SellItem(Item item)
        {
            Console.Clear();
            string isEquip = string.Empty;
            CheckEquipItem();
            if (item.Equals(Weapon))
            {
                Weapon = new Item();
                player.EquipItem(ItemType.WEAPON, null);
                isEquip = "착용한";
            }
            if (item.Equals(Helmet))
            {
                Helmet = new Item();
                player.EquipItem(ItemType.HELMET, null);
                isEquip = "착용한";
            }
            if (item.Equals(Armor))
            {
                Armor = new Item();
                player.EquipItem(ItemType.ARMOR, null);
                isEquip = "착용한";
            }
            if (item.Equals(Shoes))
            {
                Shoes = new Item();
                player.EquipItem(ItemType.SHOES, null);
                isEquip = "착용한";
            }
            player._inventory.Remove(item);
            foreach(Item _item in itemdata)
            {
                if(_item._name == item._name)
                {
                    _item._isbuy = false;
                }
            }
            player._gold += (int)Math.Round(item._price * 0.85f);
            Console.WriteLine($"{isEquip} {item._name}을 판매해 {item._price * 0.85f}G를 획득했습니다.\n");

        }

    }
}
