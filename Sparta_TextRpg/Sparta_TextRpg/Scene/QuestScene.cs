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
        private List<Quest> quests;
        public override void Enter()
        {
            quests = DataManager.Instance.Quests;
            sceneName = SceneName.QuestScene;
            ViewMenu();
        }
        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Console.WriteLine("Quest!!\n");
            int cnt = 1;
            //수행중 표시여부
            foreach (Quest quest in quests)
            {
                bool isAccept = false;
                foreach (Quest playerQuest in GameManager.Instance.player._quest)
                {
                    if (playerQuest.title == quest.title)
                    {
                        isAccept = true;
                        break;
                    }
                }
                string Acceptment = isAccept ? "[수행중인]" : "";
                Console.WriteLine($"{cnt}. {Acceptment} {quest.title}");
                cnt++;
            }
            Console.WriteLine("");
            Console.WriteLine("\n원하시는 퀘스트를 선택해 주세요.");
            Console.WriteLine("\n0. 나가기");
            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key < ConsoleKey.D1 + quests.Count)
            {
                Console.Clear();
                int idx = (int)(key - 49);
                ShowQuest(idx);
            }
            else if (key >= ConsoleKey.NumPad1 && key < ConsoleKey.NumPad1 + quests.Count)
            {
                Console.Clear();
                int idx = (int)(key - 97);
                ShowQuest(idx);
            }
            else if (key == ConsoleKey.D0 && key == ConsoleKey.D0 )
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

        private void ShowQuest(int idx)
        {
            Quest quest = quests[idx];
            if (quest.curcnt >= quest.goalcnt)
            {
                RewardQuest(idx);
            }
            else
            {
                AcceptQuest(idx);
            }
        }
        private void AcceptQuest(int idx)
        {
            Quest quest = quests[idx];
            Console.WriteLine("Quest!!\n");
            Console.WriteLine(quest.title);
            Console.WriteLine("\n" + quest.description);
            Console.WriteLine($"\n- {quest.goal} ({quest.curcnt}/{quest.goalcnt})");
            Console.WriteLine($"\n- 보상 - \n{quest.reward._name} \n {quest.gold}");

            Console.WriteLine($"\n1. 수락");
            Console.WriteLine($"2. 거절");

            bool isAccept = false;
            foreach (Quest playerQuest in GameManager.Instance.player._quest)
            {
                if (playerQuest.title == quest.title)
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
                        Console.WriteLine($"\n이미 수행중인 퀘스트입니다.");
                    else
                        GameManager.Instance.player._quest.Add(quest);
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
                    ShowQuest(idx);
                    break;
            }
        }
        private void RewardQuest(int idx)
        {
            Quest quest = quests[idx];

            Console.WriteLine("Quest!!\n");
            Console.WriteLine(quest.title);
            Console.WriteLine("\n" + quest.description);
            Console.WriteLine($"\n- {quest.goal} ({quest.curcnt}/{quest.goalcnt})");
            Console.WriteLine($"\n- 보상 - \n{quest.reward._name} \n {quest.gold}");

            Console.WriteLine($"\n1. 보상받기");
            Console.WriteLine($"2. 돌아가기");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    if (quest.curcnt >= quest.goalcnt)
                    {
                        GameManager.Instance.player._inventory.Add(quest.reward);
                        GameManager.Instance.player._gold += quest.gold;
                        GameManager.Instance.player._quest.Remove(quest);
                        ViewMenu();
                    }
                    else
                    {
                        Console.WriteLine("조건이 충족되지 않습니다 조건을 확인해주세요");
                        ShowQuest(idx);
                    }
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    ViewMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    ShowQuest(idx);
                    break;
            }
        }
    }
}
