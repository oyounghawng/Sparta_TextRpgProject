using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg.Scene
{
    internal class QuestScene : BaseScene
    {
        private Player player;
        private List<Quest> questsData;
        private List<Quest> playerquestlist;
        public override void Enter()
        {
            player = GameManager.Instance.player;
            questsData = DataManager.Instance.Quests;
            sceneName = SceneName.QuestScene;
            playerquestlist = GameManager.Instance.player._quest;
            ViewMenu();
        }
        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Utility.PrintTextHighlights("- ", "Quest !", " - \n", ConsoleColor.Red);
            Utility.PrintTextHighlights("[", "퀘스트목록", "]", ConsoleColor.Green);
            int cnt = 1;
            Console.WriteLine(Utility.PadRightForMixedText("-  난이도", 10)
            + Utility.PadRightForMixedText($"퀘스트 내용", 15));
            Console.WriteLine("------------------------------------------------------------------------------------------");

            //수행중 표시여부
            foreach (Quest quest in questsData)
            {
                bool isAccept = false;
                bool isClear = false;
                string condition = string.Empty;
                if (playerquestlist.Count > 0)
                {
                    foreach (Quest playerQuest in GameManager.Instance.player._quest)
                    {
                        if (playerQuest.title == quest.title && playerQuest.difficulty == quest.difficulty)
                        {
                            if (playerQuest.curcnt >= playerQuest.goalcnt)
                            {
                                isClear = true;
                                break;
                            }
                            else
                            {
                                isAccept = true;
                                break;
                            }
                        }
                    }
                }
                if (isAccept)
                {
                    if (isClear)
                        condition = "[완료]";
                    else
                        condition = "[진행중]";
                }
                if (isClear)
                {
                    Utility.PrintTextHighlights("", Utility.PadRightForMixedText($"-{cnt}. [{quest.difficulty}]", 10)
                    + Utility.PadRightForMixedText($"{quest.title}", 15)
                    + Utility.PadRightForMixedText($"{condition}", 10), "", ConsoleColor.Magenta);
                }
                else
                {
                    Console.WriteLine(Utility.PadRightForMixedText($"-{cnt}. [{quest.difficulty}]", 10)
                    + Utility.PadRightForMixedText($"{quest.title}", 15)
                    + Utility.PadRightForMixedText($"{condition}", 10));
                }
                cnt++;
            }
            Console.WriteLine("\n원하시는 퀘스트를 선택해 주세요.");
            Console.WriteLine("\n0. 나가기");
            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key < ConsoleKey.D1 + questsData.Count)
            {
                Console.Clear();
                int idx = (int)(key - 49);
                CheckQuestCondition(idx);
            }
            else if (key >= ConsoleKey.NumPad1 && key < ConsoleKey.NumPad1 + questsData.Count)
            {
                Console.Clear();
                int idx = (int)(key - 97);
                CheckQuestCondition(idx);
            }
            else if (key == ConsoleKey.D0 && key == ConsoleKey.D0)
            {
                Console.Clear();
                GameManager.Instance.ChangeScene(SceneName.StartScene);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                GameManager.Instance.LoadPreScene();
            }
        }
        private void CheckQuestCondition(int idx)
        {
            foreach (Quest playerQuest in GameManager.Instance.player._quest)
            {
                if (playerQuest.title == questsData[idx].title && playerQuest.difficulty == questsData[idx].difficulty)
                {
                    ViewQuestCurrent(playerQuest);
                }
            }
            ViewNewQuest(idx);
        }
        private void ViewNewQuest(int idx)
        {
            Quest quest = questsData[idx].DeepCopy(questsData[idx]);
            Utility.PrintTextHighlights("- ", "Quest !", " - \n", ConsoleColor.Red);
            Console.Write(quest.title);
            Utility.PrintTextHighlights(" [", quest.difficulty, "]", ConsoleColor.Green);
            Console.WriteLine("\n" + quest.description);
            Console.WriteLine($"\n- {quest.goal} ({quest.curcnt}/{quest.goalcnt})");
            Utility.PrintTextHighlights("보상 : ", quest.reward, "", ConsoleColor.Magenta);
            Console.WriteLine($"\n1. 수락");
            Console.WriteLine($"2. 거절");

            bool isAccept = false;
            foreach (Quest playerQuest in playerquestlist)
            {
                if (playerQuest.title == quest.title && playerQuest.difficulty == quest.difficulty)
                {
                    isAccept = true;
                    break;
                }
            }
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    if (isAccept)
                        Utility.PrintTextHighlights("", "이미 수행중인 퀘스트입니다.", "", ConsoleColor.Red);
                    else if (!player.AddQuest())
                        Utility.PrintTextHighlights("", "퀘스트는 한번에 3개 까지만 받을 수 있습니다.!", "", ConsoleColor.Red);
                    else
                        playerquestlist.Add(quest);
                    ViewMenu();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    ViewMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ViewNewQuest(idx);
                    break;
            }
        }
        private void ViewQuestCurrent(Quest quest)
        {
            Utility.PrintTextHighlights("- ", "Quest!", " - \n", ConsoleColor.Red);
            Console.Write(quest.title);
            Utility.PrintTextHighlights(" [", quest.difficulty, "]", ConsoleColor.Green);
            Console.WriteLine("\n" + quest.description);
            Console.WriteLine($"\n- {quest.goal} ({quest.curcnt}/{quest.goalcnt})");
            Utility.PrintTextHighlights("보상 : ", quest.reward, "", ConsoleColor.Magenta);
            Utility.PrintTextHighlights("보상 : ", quest.gold.ToString(), "", ConsoleColor.Magenta);
            //보상 조건 충족
            if (quest.curcnt < quest.goalcnt)
            {
                Console.WriteLine($"\n0. 나가기");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        Console.Clear();
                        ViewMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        ViewQuestCurrent(quest);
                        break;
                }
            }
            //조건 미충족
            else
            {
                Console.WriteLine($"\n 보상을 받으시려면 아무키나 눌르세요");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    default:
                        Console.Clear();
                        GameManager.Instance.player._gold += quest.gold;

                        if (quest.reward.Contains("장비"))
                            GetGearItem(quest);
                        else
                            GetConsumableItem(quest);
                        playerquestlist.Remove(quest);
                        ViewMenu();
                        break;
                }
            }

        }
        private void GetGearItem(Quest quest)
        {
            ItemRating itemrating = ItemRating.UNKOWN;
            switch(quest.difficulty)
            {
                case "하" :
                    itemrating = ItemRating.RARE;
                    break;
                case "중":
                    itemrating = ItemRating.UNIQUE;
                    break;
                case "상":
                    itemrating = ItemRating.LEGEND;
                    break;
            }
            List<Item> filterGearItem = DataManager.Instance.Items.Where(item =>
            (item._itemtype == ItemType.WEAPON || item._itemtype == ItemType.HELMET ||
            item._itemtype == ItemType.ARMOR || item._itemtype == ItemType.SHOES) && item._itemrating == itemrating).ToList();

            Random random = new Random();
            int idx = random.Next(0,filterGearItem.Count);
            Item temp = filterGearItem[idx].DeepCopy(filterGearItem[idx]);
            player._inventory.Add(temp);
            Utility.PrintTextHighlights("보상으로 [", temp._name, "]를 획득했습니다.", ConsoleColor.Green);

        }
        private void GetConsumableItem(Quest quest)
        {
            ItemRating itemrating = ItemRating.UNKOWN;
            switch (quest.difficulty)
            {
                case "하":
                    itemrating = ItemRating.RARE;
                    break;
                case "중":
                    itemrating = ItemRating.UNIQUE;
                    break;
                case "상":
                    itemrating = ItemRating.LEGEND;
                    break;
            }
            List<Item> filterItem = DataManager.Instance.Items.Where(item => item._itemtype == ItemType.POTION && item._itemrating == itemrating).ToList();
            Item temp = new Item();
            if (quest.reward.Contains("체력"))
                temp  = filterItem[0].DeepCopy(filterItem[0]);
            else
                temp = filterItem[1].DeepCopy(filterItem[1]);

            Item potion = player._inventory.FirstOrDefault(item => item._name == temp._name);
            if (potion != null)
            {
                potion._cnt += 1;
            }
            else
            {
                player._inventory.Add(temp);
            }
            Utility.PrintTextHighlights("보상으로 [", temp._name, "]를 획득했습니다.", ConsoleColor.Green);

        }
    }
}
