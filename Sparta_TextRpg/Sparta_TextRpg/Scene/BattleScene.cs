using Sparta_TextRpg.Scene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sparta_TextRpg
{
    internal class BattleScene : BaseScene
    {
        private List<Enemy> enemies;
        private List<Enemy> enemydata;
        private List<Quest> quests;
        private Player player;
        private int playerpreBattleHp;
        private bool IsBattle = false;
        Item Weapon;
        Item Helmet;
        Item Armor;
        Item Shoes;
        public override void Enter()
        {
            sceneName = SceneName.BattleScene;
            Random random = new Random();

            player = GameManager.Instance.player;
            playerpreBattleHp = player._currenthp;
            quests = player._quest;

            enemydata = DataManager.Instance.Enemys;
            enemies = new List<Enemy>();
            int enemydatacount = enemydata.Count;
            int enemycount = random.Next(1, 5);
            for (int i = 0; i < enemycount; i++)
            {
                int enemyidx = random.Next(0, 3);
                Enemy enemy = enemydata[enemyidx].DeepCopy(enemydata[enemyidx]);
                enemy.SetLevelStat(player._level);
                enemies.Add(enemy);

            }
            ViewMenu();
        }
        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Utility.PrintTextHighlights("- ", "Battle !", " - \n", ConsoleColor.Red);
            foreach (Enemy enemy in enemies)
            {
                string Diestring = !enemy.isDie ? enemy.hp.ToString() : "Dead";
                Console.WriteLine(Utility.PadRightForMixedText($"- Lv.{enemy.level} {enemy.name}", 20)
                + Utility.PadRightForMixedText($"HP : {Diestring}", 15)
                + Utility.PadRightForMixedText($"Atk : {enemy.atk}", 15)
                + Utility.PadRightForMixedText($"Def : {enemy.def}", 15));
            }
            ShowPlayerStat();
            Console.WriteLine("1. 전투시작");
            Console.WriteLine("0. 도망가기");
            //행동 선택
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    AttackMenu();
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
        private void ShowPlayerStat()
        {
            CheckEquipItem();
            Utility.PrintTextHighlights("", "\n[내정보]", "", ConsoleColor.Green);
            Console.WriteLine(Utility.PadRightForMixedText("이름", 10) + " : " + player._name);
            Console.WriteLine(Utility.PadRightForMixedText("Lv", 10) + " : " + player._level.ToString("D2"));
            Console.WriteLine(Utility.PadRightForMixedText("Chad", 10) + " : " + player._playerjobs._playerjob);
            string weaponStat = Weapon != null ? $" (+{Weapon._statvalue})" : string.Empty;
            Console.WriteLine(Utility.PadRightForMixedText("공격력", 10) + " : " + player.Attack + weaponStat);
            string HelmetStat = Helmet != null ? $"( {Helmet._name} : +{Helmet._statvalue} )" : string.Empty;
            string ArmorStat = Armor != null ? $"( {Armor._name} : +{Armor._statvalue} )" : string.Empty;
            string ShoesStat = Shoes != null ? $"( {Shoes._name} : +{Shoes._statvalue} )" : string.Empty;
            Console.WriteLine(Utility.PadRightForMixedText("방어력", 10) + " : " + player.Deffence + HelmetStat + ArmorStat + ShoesStat);
            Console.WriteLine(Utility.PadRightForMixedText("체력", 10) + " : " + $"{player.HP} / {player._maxhp}");
            Console.WriteLine(Utility.PadRightForMixedText("마나", 10) + " : " + $"{player.MP} / {player._maxmp}");
            Console.WriteLine(Utility.PadRightForMixedText("경험치", 10) + " : " + $"{player._exp} / {player._needlevelexp[player._level - 1]}");
            Console.WriteLine(Utility.PadRightForMixedText("크리티컬 확률", 13) + " : " + player.Critical + "%");
            Console.WriteLine(Utility.PadRightForMixedText("회피 확률", 13) + " : " + player.Dodge + "%\n");
        }
        private void ShowEnemyStat()
        {
            int cnt = 1;
            foreach (Enemy enemy in enemies)
            {
                string Diestring = !enemy.isDie ? enemy.hp.ToString() : "Dead";
                Console.Write(Utility.PadRightForMixedText($"{cnt}. Lv.{enemy.level} {enemy.name}", 20) + "HP : ");
                if (enemy.isDie)
                    Utility.PrintTextHighlights("", Diestring, " ", ConsoleColor.Red);
                else
                    Console.WriteLine(enemy.hp.ToString());
                cnt++;
            }
        }
        private void AttackMenu()
        {
            Utility.PrintTextHighlights("- ", "Battle - PlayerTurn!", " - \n", ConsoleColor.Red);
            ShowEnemyStat();
            ShowPlayerStat();
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {i + 1}번적 공격하기");
            }
            Utility.PrintTextHighlights("A. ", "스킬사용하기", "", ConsoleColor.Green);
            Utility.PrintTextHighlights("S. ", "물약사용하기", "", ConsoleColor.Magenta);

            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key < ConsoleKey.D1 + enemies.Count)
            {
                Console.Clear();
                int idx = (int)(key - 49);
                if (!enemies[idx].isDie)
                    PlayerAttack(idx);
                else
                {
                    Console.WriteLine("이미 죽은 몬스터입니다. 다른 몬스터를 선택해 주세요");
                    AttackMenu();
                }
            }
            else if (key >= ConsoleKey.NumPad1 && key < ConsoleKey.NumPad1 + enemies.Count)
            {
                Console.Clear();
                int idx = (int)(key - 97);
                if (!enemies[idx].isDie)
                    PlayerAttack(idx);
                else
                {
                    Console.WriteLine("이미 죽은 몬스터입니다. 다른 몬스터를 선택해 주세요");
                    AttackMenu();
                }
            }
            else if (key == ConsoleKey.A)
            {
                Console.Clear();
                Skill();
            }
            else if (key == ConsoleKey.S)
            {
                Console.Clear();
                ViewPotion();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                AttackMenu();
            }
        }

        #region Player
        private void PlayerAttack(int idx)
        {
            bool critic = false;
            Random random = new Random();
            int critical = random.Next(1, 101);// 치명타
            if (critical <= player.Critical)
            {
                critic = true;

            }
            else
            {
                critic = false;
            }
            float damage = player.Attack;
            float offset = MathF.Round(damage * 0.1f);
            int offsetdamage = random.Next((int)(damage - offset), (int)(damage + offset + 1));
            int preEnemiseHp = enemies[idx].HP;
            if (critic == true)
            {
                offsetdamage = (int)MathF.Round(1.6f * (offsetdamage));
            }
            enemies[idx].HP = offsetdamage;

            Utility.PrintTextHighlights("- ", "Battle - PlayerTurn!", " - \n", ConsoleColor.Red);
            Console.WriteLine(player._name + "의 공격!\n");
            Console.Write($"Lv.{enemies[idx].level} {enemies[idx].name} 을(를) 맞췄습니다.");
            Utility.PrintTextHighlights("[", $"데미지 : {offsetdamage}", "] \n", ConsoleColor.Red);

            //적 공격 표시
            for (int i = 0; i < enemies.Count; i++)
            {
                string Diestring = !enemies[i].isDie ? enemies[i].hp.ToString() : "Dead";
                //타격된 적
                if (idx == i)
                {
                    Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                    Utility.PrintTextHighlights($"HP {preEnemiseHp} -> ", Diestring, "", ConsoleColor.Red); ;
                }
                else
                {
                    Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                    Console.WriteLine($"HP {enemies[i].HP}");
                }
            }
            IsEndBattle();
        }
        private void Skill()
        {
            Utility.PrintTextHighlights("- ", "Battle - PlayerTurn!", " - \n", ConsoleColor.Red);
            ShowEnemyStat();
            ShowPlayerStat();

            Utility.PrintTextHighlights("1.", player._playerjobs.Skill1_Name, "", ConsoleColor.Blue);
            Console.WriteLine(Utility.PadRightForMixedText($"MP를 10 소모", 15)
            + $"공격력 * 2 ({player.Attack * 2})로 하나의 적을 공격합니다.\n");

            Utility.PrintTextHighlights("2.", player._playerjobs.Skill2_Name, "", ConsoleColor.White);
            Console.WriteLine(Utility.PadRightForMixedText($"MP를 15 소모", 15)
            + $"공격력 * 1.5 ({player.Attack * 1.5})로 두명의 적을 공격합니다.\n");

            Console.WriteLine("0. 취소");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    Skill1Target();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Skill2Active();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    AttackMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Skill();
                    break;
            }
            ShowPlayerStat();
            IsEndBattle();
        }
        private void Skill1Target()
        {
            Utility.PrintTextHighlights("- ", "Battle - PlayerTurn!", " - \n", ConsoleColor.Red);
            ShowEnemyStat();
            Console.WriteLine("\n타겟을 선택하세요\n");
            if (player._currentmp >= 10)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    Console.WriteLine($"{i + 1}번적에게 {player._playerjobs.Skill1_Name}사용하기");
                }
                Console.WriteLine("\n0. 돌아가기");

                var key2 = Console.ReadKey(true).Key;
                if (key2 >= ConsoleKey.D1 && key2 < ConsoleKey.D1 + enemies.Count)
                {
                    Console.Clear();
                    int idx = (int)(key2 - 49);
                    Skill1Active(idx);
                }
                else if (key2 >= ConsoleKey.NumPad1 && key2 < ConsoleKey.NumPad1 + enemies.Count)
                {
                    Console.Clear();
                    int idx = (int)(key2 - 97);
                    Skill1Active(idx);
                }
                else if (key2 == ConsoleKey.D0 || key2 == ConsoleKey.NumPad0)
                {
                    Console.Clear();
                    Skill();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("MP가 부족합니다.");
                AttackMenu();
            }
        }
        private void Skill1Active(int idx)
        {
            if (enemies[idx].isDie)
            {
                Console.Clear();
                Console.WriteLine("이미 죽은 몬스터입니다. 다른 몬스터를 선택해 주세요");
                Skill1Target();
            }
            int damage = (int)MathF.Round(2 * player.Attack);
            int preEnemyhp = enemies[idx].HP;
            enemies[idx].HP = damage;
            player.MP = 10;
            string isDieString = !enemies[idx].isDie ? enemies[idx].HP.ToString() : "Dead";
            //적 공격 표시
            Utility.PrintTextHighlights("", $"{player._playerjobs.Skill1_Name}", "", ConsoleColor.Blue);
            Utility.PrintTextHighlights("[", $"데미지 : {damage}", "] \n", ConsoleColor.Red);
            Console.WriteLine("MP 10소모");
            for (int i = 0; i < enemies.Count; i++)
            {
                string Diestring = !enemies[i].isDie ? enemies[i].hp.ToString() : "Dead";
                //타격된 적
                if (idx == i)
                {
                    Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                    Utility.PrintTextHighlights($"HP {preEnemyhp} -> ", Diestring, "", ConsoleColor.Red); ;
                }
                else
                {
                    Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                    Console.WriteLine($"HP {enemies[i].HP}");
                }
            }
        }
        private void Skill2Active()
        {
            int[] targetidx = new int[2] { -1, -1 };
            int[] preEnemyhp = new int[2];
            int damage = (int)MathF.Round(1.5f * player.Attack];
            Random random = new Random();
            int liveEnemyCount = 0;
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isDie)
                    liveEnemyCount++;
            }

            if (player._currentmp >= 15)
            {
                if (liveEnemyCount >= 2)
                {
                    Utility.PrintTextHighlights("", $"{player._playerjobs.Skill2_Name}", "", ConsoleColor.White);
                    Console.WriteLine("MP 15소모");
                    player.MP = 15;
                    int cnt = 0;
                    while (cnt < 2)
                    {
                        int idx = random.Next(enemies.Count);
                        Enemy enemy = enemies[idx];
                        if (enemy.isDie)
                            continue;

                        if (targetidx[0] == idx)
                            continue;

                        if (cnt == 0)
                        {
                            targetidx[0] = idx;
                            preEnemyhp[0] = enemy.HP;
                        }
                        if (cnt == 1)
                        {
                            targetidx[1] = idx;
                            preEnemyhp[1] = enemy.HP;
                        }
                        enemy.HP = damage;
                        cnt++;

                    }
                }
                else
                {
                    Utility.PrintTextHighlights("", $"{player._playerjobs.Skill2_Name}", "", ConsoleColor.White);
                    Console.WriteLine("MP 15소모");
                    player.MP = 15;
                    int cnt = 0;
                    while (cnt < 1)
                    {
                        int idx = random.Next(enemies.Count);
                        Enemy enemy = enemies[idx];
                        if (enemy.isDie)
                            continue;

                        targetidx[0] = idx;
                        preEnemyhp[0] = enemy.HP;
                        enemy.HP = damage;
                        cnt++;
                    }
                }
                //적 공격 표시
                Utility.PrintTextHighlights("[", $"데미지 : {damage}", "] \n", ConsoleColor.Red);
                for (int i = 0; i < enemies.Count; i++)
                {
                    string Diestring = !enemies[i].isDie ? enemies[i].hp.ToString() : "Dead";
                    //타격된 적
                    if (targetidx[0] == i)
                    {
                        Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                        Utility.PrintTextHighlights($"HP {preEnemyhp[0]} -> ", Diestring, "", ConsoleColor.Red); ;
                    }
                    else if (targetidx[1] == i)
                    {
                        Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                        Utility.PrintTextHighlights($"HP {preEnemyhp[1]} -> ", Diestring, "", ConsoleColor.Red); ;
                    }
                    else
                    {
                        Console.Write(Utility.PadRightForMixedText($"- Lv.{enemies[i].level} {enemies[i].name}", 20));
                        Console.WriteLine($"HP {enemies[i].HP}");
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("MP가 부족합니다.");
                AttackMenu();
            }
        }
        #endregion
        //적 공격
        private void EnemyAttack()
        {
            Random random = new Random();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].isDie)
                    continue;
                Console.Clear();
                Utility.PrintTextHighlights("- ", "Battle - EnemyTurn!", " - \n", ConsoleColor.Red);
                for (int j = 0; j < enemies.Count; j++)
                {
                    Enemy enemy = enemies[j];
                    string Diestring = !enemy.isDie ? enemy.hp.ToString() : "Dead";
                    if (i == j)
                    {
                        Utility.PrintTextHighlights("", Utility.PadRightForMixedText($"{j + 1}. Lv.{enemy.level} {enemy.name}", 20)
                        + "" + Utility.PadRightForMixedText($"HP : {Diestring} [공격]", 15), "", ConsoleColor.Yellow);
                    }
                    else
                    {
                        if (enemy.isDie)
                        {
                            Console.Write(Utility.PadRightForMixedText($"{j + 1}. Lv.{enemy.level} {enemy.name}", 20) + "HP : ");
                            Utility.PrintTextHighlights("", Diestring, " ", ConsoleColor.Red);
                        }
                        else
                        {
                            Console.WriteLine(Utility.PadRightForMixedText($"{j + 1}. Lv.{enemy.level} {enemy.name}", 20) + $"HP : {Diestring}");
                        }

                    }
                }
                bool avoidance = false;
                int eatk = enemies[i].atk;
                int avoid = random.Next(1, 101);// 회피
                if (avoid <= player.Dodge)
                {
                    avoidance = true;
                }
                else
                {
                    avoidance = false;
                }

                if (avoidance == true)
                {
                    eatk = 0 * (enemies[i].atk);
                    Utility.PrintTextHighlights("\n", "'느려'", "", ConsoleColor.Yellow);
                    avoidance = false;
                }
                int preEnemyAttackHp = player.HP;
                Console.WriteLine($"\n[데미지 : {eatk}]를 입었습니다.");
                player.HP = eatk;
                Console.WriteLine($"HP {preEnemyAttackHp} -> {player.HP} \n");
                Console.WriteLine("0. 다음");

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        continue;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
            Console.Clear();
            int cnt = 1;
            Utility.PrintTextHighlights("- ", "Battle - EnemyTurn!", " - \n", ConsoleColor.Red);
            foreach (Enemy enemy in enemies)
            {
                string Diestring = !enemy.isDie ? enemy.hp.ToString() : "Dead";
                Console.Write(Utility.PadRightForMixedText($"{cnt}. Lv.{enemy.level} {enemy.name}", 20) + "HP : ");
                if (enemy.isDie)
                    Utility.PrintTextHighlights("", Diestring, " ", ConsoleColor.Red);
                else
                    Console.WriteLine(enemy.hp.ToString());
                cnt++;
            }
            Utility.PrintTextHighlights("", "\n모든 적의 공격이 끝났습니다.", "", ConsoleColor.Green);
            Console.WriteLine("0. 다음");

            var key2 = Console.ReadKey(true).Key;
            while (key2 != ConsoleKey.D0 && key2 != ConsoleKey.NumPad0)
            {
                key2 = Console.ReadKey(true).Key;
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.Clear();
            AttackMenu();
        }

        #region Result
        private void ViewBattleVictoryResult()
        {
            Utility.PrintTextHighlights("", "Victory!! - Result", "", ConsoleColor.Red);
            Console.WriteLine($"던전에서 몬스터 {enemies.Count}마리를 잡았습니다.");
            int AllExp = 0;
            foreach (Enemy enemy in enemies)
            {
                if (player._level <= player._needlevelexp.Length)
                {
                    player._exp += enemy.exp;
                    AllExp += enemy.exp;
                }
                if (quests.Count > 0)
                {
                    foreach (Quest quest in quests)
                    {
                        quest.cntQuest(enemy);
                    }
                }
            }
            Utility.PrintTextHighlights("경험치를 ", AllExp.ToString(), " 획득하였습니다.", ConsoleColor.Magenta);
            if (player._level < player._needlevelexp.Length)
            {
                while (player._exp >= player._needlevelexp[player._level - 1])
                {
                    player._exp -= player._needlevelexp[player._level - 1];
                    player.LevelUP();
                    Utility.PrintTextHighlights("", $"LV Up!! {player._level} -> {++player._level}", "", ConsoleColor.Magenta);
                    Console.WriteLine($"공격력 0.5 상승  방어력 : 1상승");
                    if (player._level - 1 >= player._needlevelexp.Length)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine($"\n마나를 10 회복하였습니다.");
            player.MP = 10;
            ShowPlayerStat();
            Reward();
            Console.WriteLine($"\n0. 로비로");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.ChangeScene(SceneName.StartScene);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
        private void Reward()
        {
            Random random = new Random();

            foreach (Enemy enemy in enemies)
            {
                int rand = random.Next(1, 101);

                if (rand <= 90) // 90프로 확률로 골드 획득
                {
                    int rewardGold = random.Next(10, 101);
                    player._gold += rewardGold;
                    Console.WriteLine($"[골드]을(를) {rewardGold} 획득하였습니다");
                }


                rand = random.Next(1, 101);
                if (rand <= 99) // 20프로 확률로 아이템 획득
                {
                    rand = random.Next(1, 101);
                    List<Item> filterItem;
                    ItemRating rating;
                    if (rand <= 70) // 70프로 확률로 물약 획득
                    {
                        filterItem = DataManager.Instance.Items.Where(item => item._itemtype == ItemType.POTION).ToList();
                    }
                    else // 나머지 30프로 확률로 아이템 획득
                    {
                        filterItem = DataManager.Instance.Items.Where(item => item._itemtype == ItemType.WEAPON
                        || item._itemtype == ItemType.ARMOR
                        || item._itemtype == ItemType.SHOES
                        || item._itemtype == ItemType.HELMET).ToList();
                    }
                    rand = random.Next(1, 101);
                    if (rand <= 70)
                    {
                        rating = ItemRating.RARE;
                    }
                    else if (rand >= 71 && rand <= 90)
                    {
                        rating = ItemRating.RARE;
                    }
                    else if (rand >= 91 && rand <= 99)
                    {
                        rating = ItemRating.UNIQUE;
                    }
                    else
                    {
                        if (filterItem.Any(item => item._itemtype == ItemType.WEAPON))
                        {
                            rating = ItemRating.LEGEND;
                        }
                        else
                        {
                            rating = ItemRating.RARE;
                        }
                    }

                    filterItem = filterItem.Where(item => item._itemrating == rating).ToList();

                    if (filterItem.Count > 0)
                    {
                        int randomIndex = random.Next(0, filterItem.Count);
                        Item randomItem = filterItem[randomIndex];

                        // 물약인 경우 수량 조정
                        if (randomItem._itemtype == ItemType.POTION)
                        {

                            Item potion = player._inventory.FirstOrDefault(item => item._name == randomItem._name && item._itemtype == ItemType.POTION);
                            if (potion != null)
                            {
                                potion._cnt += 1;
                            }
                            else
                            {
                                player._inventory.Add(randomItem.DeepCopy(randomItem));
                            }
                        }
                        else
                        {
                            player._inventory.Add(randomItem.DeepCopy(randomItem));
                        }
                        if (randomItem._itemrating == ItemRating.RARE)
                        {
                            Utility.PrintTextHighlights("", $"[{randomItem._name}]을(를) 획득하였습니다", "\n", ConsoleColor.White);
                        }
                        else if (randomItem._itemrating == ItemRating.UNIQUE)
                        {
                            Utility.PrintTextHighlights("", $"[{randomItem._name}]을(를) 획득하였습니다", "\n", ConsoleColor.Yellow);
                        }
                        else if (randomItem._itemrating == ItemRating.LEGEND)
                        {
                            Utility.PrintTextHighlights("", $"[{randomItem._name}]을(를) 획득하였습니다", "\n", ConsoleColor.Green);
                        }

                    }
                }
            }
        }
        private void IsEndBattle()
        {
            bool isEndBattle = true;
            //전투 종료 판단
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isDie)
                {
                    isEndBattle = false;
                    break;
                }
                else
                    isEndBattle = true;
            }
            //모든 적의 사망확인
            if (isEndBattle)
            {
                Console.WriteLine("\n모든적이 사망했습니다.");
                Console.WriteLine("0. 결과창으로 이동\n");
                var key = Console.ReadKey(true).Key;
                while (key != ConsoleKey.D0 && key != ConsoleKey.NumPad0)
                {
                    key = Console.ReadKey(true).Key;
                    Console.WriteLine("잘못된 입력입니다.");
                }
                Console.Clear();
                ViewBattleVictoryResult();
            }
            else
            {
                Console.WriteLine("\n적의 공격 턴입니다.");
                Console.WriteLine("0. 다음\n");
                var key = Console.ReadKey(true).Key;
                while (key != ConsoleKey.D0 && key != ConsoleKey.NumPad0)
                {
                    key = Console.ReadKey(true).Key;
                    Console.WriteLine("잘못된 입력입니다.");
                }
                Console.Clear();
                EnemyAttack();
            }
        }
        #endregion

        #region UsePotion
        private List<Item> potioninventory;
        private void ViewPotion()
        {
            Utility.PrintTextHighlights("- ", "Battle - PlayerTurn!", " - \n", ConsoleColor.Red);
            potioninventory = player._inventory.Where(item => item._itemtype == ItemType.POTION).ToList();
            Console.WriteLine("[보유중인 물약]");
            //여기 좀 수정할수있으면 하기
            for (int i = 0; i < potioninventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potioninventory[i]._name} 회복량 : {potioninventory[i]._statvalue} 보유갯수 : {potioninventory[i]._cnt}");
            }

            Console.WriteLine("\n사용할 물약을 선택해주세요");
            ShowPlayerStat();
            Console.WriteLine("\n0. 돌아가기");
            // 키 입력받기
            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key <= ConsoleKey.D1 + potioninventory.Count)
            {
                Console.Clear();
                int idx = (int)(key - 49);
                PotionHeal(idx);
            }
            else if (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad1 + potioninventory.Count)
            {
                Console.Clear();
                int idx = (int)(key - 97);
                PotionHeal(idx);
            }
            else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
            {
                Console.Clear();
                AttackMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                ViewPotion();
            }
        }
        private void PotionHeal(int idx)
        {
            Utility.PrintTextHighlights("- ", "Battle - PlayerTurn!", " - \n", ConsoleColor.Red);
            Item potion = potioninventory[idx];
            string HpOrMp = string.Empty;
            int curHpOrMp = 0;
            int afterHpOrMp = 0;
            if (potion._name.Contains("체력"))
            {
                if (player._maxhp == player.HP)
                {
                    Console.Clear();
                    Utility.PrintTextHighlights("", "최대 체력입니다.", " - \n", ConsoleColor.Red);
                    ViewPotion();
                }
                HpOrMp = "HP";
                curHpOrMp = player.HP;
                player.HealHP = potion._statvalue;
                afterHpOrMp = player.HP;
            }
            else
            {
                if (player._maxmp == player.MP)
                {
                    Console.Clear();
                    Utility.PrintTextHighlights("", "최대 마나입니다.", " - \n", ConsoleColor.Red);
                    ViewPotion();
                }
                HpOrMp = "MP";
                curHpOrMp = player.MP;
                player.HealMP = potion._statvalue;
                afterHpOrMp = player.MP;
            }
            potion._cnt--;
            Utility.PrintTextHighlights($"{HpOrMp} 를 [", $"{potion._statvalue}", "] 회복하였습니다.", ConsoleColor.Red);
            Console.WriteLine($"{HpOrMp} : {curHpOrMp} -> {afterHpOrMp}");
            ShowPlayerStat();
            IsEndBattle();
        }
        #endregion
    }
}