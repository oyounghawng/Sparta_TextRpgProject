using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class StatusScene : BaseScene
    {
        Player player;
        Item Weapon;
        Item Helmet;
        Item Armor;
        Item Shoes;
        public override void Enter()
        {
            sceneName = SceneName.StatusScene;
            player = GameManager.Instance.player;
            ViewMenu();
        }
        public override void Excute()
        {

        }

        public override void ViewMenu()
        {
            CheckEquipItem();
            Console.Clear();
            Utility.PrintTextHighlights(" - ", "상태 보기", " - ", ConsoleColor.Red);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine(Utility.PadRightForMixedText("이름", 13) + " : " + player._name);
            Console.WriteLine(Utility.PadRightForMixedText("Lv", 13) + " : " + player._level.ToString("D2"));
            Console.WriteLine(Utility.PadRightForMixedText("Chad", 13) + " : " + player._playerjobs._playerjob);
            string weaponStat = Weapon != null ? $" (+{Weapon._statvalue })" : string.Empty;
            Console.WriteLine(Utility.PadRightForMixedText("공격력", 13) + " : " + player._attack + weaponStat);
            string HelmetStat = Helmet != null ? $"( { Helmet._name} : +{Helmet._statvalue} )" : string.Empty;
            string ArmorStat = Armor != null ? $"( { Armor._name} : +{Armor._statvalue} )" : string.Empty;
            string ShoesStat = Shoes != null ? $"( { Shoes._name} : +{Shoes._statvalue} )" : string.Empty;
            Console.WriteLine(Utility.PadRightForMixedText("방어력", 13) + " : " + player._defence + HelmetStat + ArmorStat + ShoesStat);
            Console.WriteLine(Utility.PadRightForMixedText("체력", 13) + " : " + $"{player._currenthp} / {player._maxhp}");
            Console.WriteLine(Utility.PadRightForMixedText("마나", 13) + " : " + $"{player._currentmp} / {player._maxmp}");
            Console.WriteLine(Utility.PadRightForMixedText("경험치", 13) + " : " + $"{player._exp} / {player._needlevelexp[player._level - 1]}");
            Console.WriteLine(Utility.PadRightForMixedText("골드", 13) + " : " + player._gold);
            Console.WriteLine(Utility.PadRightForMixedText("크리티컬 확률", 13) + " : " + player.Critical +"%");
            Console.WriteLine(Utility.PadRightForMixedText("회피 확률", 13) + " : " + player.Dodge +"%");
            Console.WriteLine("\n1. 인벤토리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해 주세요");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.InventoryScene);
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.StartScene);
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
    }
}