using System;
using System.Collections.Generic;
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
        Item Weapon;
        Item Helmet;
        Item Armor;
        Item Shoes;
        public override void Enter()
        {
            sceneName = SceneName.StoreScene;
            player = GameManager.Instance.player;
            itemdata = DataManager.Instance.Items;
            Item Weapon = player.equipItem[ItemType.WEAPON];
            Item Helmet = player.equipItem[ItemType.HELMET];
            Item Armor = player.equipItem[ItemType.ARMOR];
            Item Shoes = player.equipItem[ItemType.SHOES];

            ViewMenu();
        }

        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player._gold + " G ");
            foreach (Item item in itemdata)
            { 
                string isBuy = string.Empty;
                if (item._isbuy)
                    isBuy = "구매완료";
                else
                    isBuy = item._price.ToString();

                Console.WriteLine($"- {item._name}     | {item._itemtype} +{item._statvalue}  | {item._description}    | {isBuy}");
            }
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    BuyStore();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
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
        private void BuyStore()
        {
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player._gold + " G ");
            int cnt = 1;
            foreach (Item item in itemdata)
            {
                string isBuy = string.Empty;
                if (item._isbuy)
                    isBuy = "구매완료";
                else
                    isBuy = item._price.ToString();

                Console.WriteLine($"- {cnt}  {item._name}     | {item._itemtype} +{item._statvalue}  | {item._description}    | {isBuy}");
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
                BuyItem((int)(key - 49));
                BuyStore();
            }
            else if ((key > ConsoleKey.NumPad0 && key <= (ConsoleKey.NumPad0 + itemdata.Count)))
            {
                BuyItem((int)(key - 97));
                BuyStore();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                BuyStore();
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
                    player.AddInventory(temp);
                    Console.Clear();
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nGold가 부족합니다.");
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
