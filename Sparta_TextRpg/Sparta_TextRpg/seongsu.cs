using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class seongsu : BaseScene
    {
        private List<Enemy> enemies;
        private Player player;
        private int playerpreBattleHp;
        private bool IsBattle = false;
        public override void Enter()
        {
            sceneName = SceneName.BattleScene;
            enemies = new List<Enemy>();
            player = GameManager.Instance.player;
            playerpreBattleHp = player._currenthp;
            player._attack = 100;
            Enemy enemy1 = new Enemy("스켈레톤");
            Enemy enemy2 = new Enemy("슬라임");
            enemies.Add(enemy1);
            enemies.Add(enemy2);
            ViewMenu();
        }
        public override void Excute()
        {

        }
        public override void ViewMenu()
        {
            Console.WriteLine("Battle!!\n");
            foreach (Enemy enemy in enemies)
            {
                Console.WriteLine(enemy.PrintEnemy(enemy));
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            Console.Write("Lv. " + player._level.ToString("D2"));
            Console.WriteLine($"   Chad.( {player._playerjobs._playerjob})");
            Console.WriteLine($"HP {player._currenthp}/{player._maxhp}");
            Console.WriteLine($"MP {player._currentmp}/{player._maxmp}\n");
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
        private void AttackMenu()
        {
            Console.WriteLine("Battle!!\n");
            int cnt = 1;
            foreach (Enemy enemy in enemies)
            {
                Console.Write(cnt + " ");
                Console.WriteLine(enemy.PrintEnemy(enemy));
                cnt++;
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            Console.Write("Lv. " + player._level.ToString("D2"));
            Console.WriteLine($"   Chad.( {player._playerjobs._playerjob})");
            Console.WriteLine($"HP {player._currenthp}/{player._maxhp}\n");

            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. 공격");
            }
            Console.WriteLine("A. 스킬사용하기");
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
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                AttackMenu();
            }

        }
        private void ViewBattleVictoryResult()
        {
            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {enemies.Count}마리를 잡았습니다.\n");


            foreach (Enemy enemy in enemies)
            {
                if (player._level <= player._needlevelexp.Length)
                {
                    player._exp += enemy.exp;
                }

            }

            if (player._level < player._needlevelexp.Length)
            {
                while (player._exp >= player._needlevelexp[player._level - 1])
                {
                    player._exp -= player._needlevelexp[player._level - 1];
                    player._level++;
                    if (player._level - 1 >= player._needlevelexp.Length)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"LV. " + player._level.ToString("D2"));
            Console.WriteLine($"{player._name} / {player._playerjobs._playerjob}");
            Console.WriteLine($"HP {player._maxhp} -> {player._currenthp}");
            Console.WriteLine($"exp: {player._exp} / {player._needlevelexp[player._level - 1]}");
            Console.WriteLine("\n[획득 아이템]");
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
        private void GameOver()
        {
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.Write("Lv. " + player._level.ToString("D2"));
            Console.WriteLine($"   Chad.( {player._playerjobs._playerjob}");
            Console.WriteLine($"HP {player._currenthp} -> 0 \n");
            return; //다시할수도?
        }
        private void EnemyAttack()
        {
            Random random = new Random();
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.Clear();
                Console.WriteLine("적의 공격 턴입니다.");
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"{i + 1}번쨰 Lv.{enemies[i].level} {enemies[i].name} 의 공격!");
                //체력 감소
                if (enemies[i].isDie)
                    continue;

                bool avoidance = false;
                int eatk = enemies[i].atk;
                int avoid = random.Next(1, 101);// 회피
                if (avoid <= 10)
                {
                    avoidance = true;
                }
                else
                {
                    avoidance = false;
                }

                if (avoidance == true)
                {
                    eatk = 0 * (enemies[0].atk);
                    Console.Write("회피하였습니다. ");
                    avoidance = false;
                }
                int preEnemyAttackHp = player.HP;
                player.HP = eatk;

                Console.WriteLine($"[데미지 : {eatk}]");
                Console.WriteLine($"HP {preEnemyAttackHp} ->{player.HP} \n");
                Console.WriteLine(player._name + "을(를) 맞췄습니다.");
                Console.WriteLine("0. 다음");

                if (i == enemies.Count - 1)
                    break;
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
            Console.WriteLine("모든 적의 공격이 끝났습니다.");
            var key2 = Console.ReadKey(true).Key;
            while (key2 != ConsoleKey.D0 && key2 != ConsoleKey.NumPad0)
            {
                key2 = Console.ReadKey(true).Key;
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.Clear();
            AttackMenu();
        }
        private void IncreaseStats()
        {
            player._attack += 0.5f; // 공격력 0.5 증가
            player._defence += 1; // 방어력 1 증가
            Console.WriteLine($"레벨업! 현재 레벨: {player._level}, 공격력: {player._attack}, 방어력: {player._defence}");
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
                if (rand <= 20) // 20프로 확률로 아이템 획득
                {
                    rand = random.Next(1, 101);
                    List<Item> filterItem;
                    ItemRating rating;

                    #region Quest
                    /*
                    //Quest 처리
                    if (player._quest[0].enemy.name == enemies[0].name)
                    {
                        player._quest[0].curcnt++;
                    }
                    */
                    #endregion
                    #region Item
                    if (rand <= 70) // 70프로 확률로 물약 획득
                    {
                        filterItem = DataManager.Instance.Items.Where(item => item._itemtype == ItemType.POTION).ToList();
                    }
                    else // 나머지 30프로 확률로 아이템 획득
                    {
                        filterItem = DataManager.Instance.Items.Where(item => item._itemtype == ItemType.WEAPON).ToList();
                    }
                    rand = random.Next(1, 101);
                    if (rand <= 70)
                    {
                        rating = ItemRating.COMMON;
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
                            rating = ItemRating.COMMON;
                        }
                    }

                    filterItem = filterItem.Where(item => item._itemrating == rating).ToList();

                    if (filterItem.Count > 0)
                    {
                        int randomIndex = random.Next(0, filterItem.Count);
                        Item randomItem = filterItem[randomIndex];
                        player._inventory.Add(randomItem);


                        Console.WriteLine($"[{randomItem._name}]을(를) 획득하였습니다");
                    }
                    #endregion
                }
            }
        }
        private void PlayerAttack(int idx)
        {
            bool critic = false;
            Random random = new Random();
            int critical = random.Next(1, 101);// 치명타
            if (critical <= 15)
            {
                critic = true;

            }
            else
            {
                critic = false;
            }
            float damage = player._attack;
            float offset = MathF.Round(damage * 0.1f);
            int offsetdamage = random.Next((int)(damage - offset), (int)(damage + offset + 1));
            int preEnemiseHp = enemies[idx].HP;
            if (critic == true)
            {
                offsetdamage = (int)MathF.Round(1.6f * (offsetdamage));
            }
            enemies[idx].HP = offsetdamage;
            Console.WriteLine("Battle!!\n");
            Console.WriteLine(player._name + " 의 공격!");
            Console.WriteLine($"Lv.{enemies[idx].level} {enemies[idx].name} 을(를) 맞췄습니다. [데미지 : {offsetdamage}]\n");
            Console.WriteLine($"Lv.{enemies[idx].level} {enemies[idx].name}");
            string isDieString = !enemies[idx].isDie ? enemies[idx].HP.ToString() : "Dead";
            Console.WriteLine($"HP {preEnemiseHp} ->{isDieString} \n");
            IsEndBattle();
        }
        private void Skill()
        {
            Console.WriteLine("Battle!!\n");
            int cnt = 1;
            foreach (Enemy enemy in enemies)
            {
                Console.Write(cnt + " ");
                Console.WriteLine(enemy.PrintEnemy(enemy));
                cnt++;
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv. {player._level}  Chad ({player._playerjobs._playerjob})");
            Console.WriteLine($"HP {player._currenthp}/{player._maxhp}");
            Console.WriteLine($"MP {player._currentmp}/{player._maxmp}\n");
            Console.WriteLine($"1. {player._playerjobs.Skill1_Name}  - MP 10");
            Console.WriteLine("   공격력 * 2 로 하나의 적을 공격합니다.");
            Console.WriteLine($"2. {player._playerjobs.Skill2_Name}- MP 15");
            Console.WriteLine("   공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
            Console.WriteLine("0. 취소");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    
                    if (player._currentmp >= 10)
                    {
                        // 알파 스트라이크
                       
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}에게. {player._playerjobs.Skill1_Name}사용하기");
                        }
                        var key2 = Console.ReadKey(true).Key;
                        if (key2 >= ConsoleKey.D1 && key2 < ConsoleKey.D1 + enemies.Count)
                        {
                            
                            Console.Clear();
                            int idx = (int)(key2 - 49);
                            if (!enemies[idx].isDie)
                            {
                                player._currentmp -= 10;
                                int damage = (int)MathF.Round(2 * player._attack);
                                int preEnemyhp = enemies[idx].HP;
                                enemies[idx].HP = damage;
                                string isDieString = !enemies[idx].isDie ? enemies[idx].HP.ToString() : "Dead";
                               // Console.WriteLine($"{player._playerjobs.Skill1_Name} 사용!\n");
                                Console.WriteLine($"Lv.{enemies[idx].level} {enemies[idx].name} 에게 {player._playerjobs.Skill1_Name}을 사용했습니다. [데미지 : {damage}]");
                                Console.WriteLine($"HP {preEnemyhp} ->{isDieString}\n");
                            }

                            else
                            {
                                Console.WriteLine("이미 죽은 몬스터입니다. 다른 몬스터를 선택해 주세요");
                                AttackMenu();
                            }
                        }
                        else if (key2 >= ConsoleKey.NumPad1 && key2 < ConsoleKey.NumPad1 + enemies.Count)
                        {
                            Console.Clear();
                            int idx = (int)(key2 - 97);
                            if (!enemies[idx].isDie)
                            {
                                player._currentmp -= 10;
                                int damage = (int)MathF.Round(2 * player._attack);
                                int preEnemyhp = enemies[idx].HP;
                                enemies[idx].HP = damage;
                                string isDieString = !enemies[idx].isDie ? enemies[idx].HP.ToString() : "Dead";
                                Console.WriteLine($"{player._playerjobs.Skill1_Name} 사용!\n");
                                Console.WriteLine($"Lv.{enemies[idx].level} {enemies[idx].name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                                Console.WriteLine($"HP {preEnemyhp} ->{isDieString}\n");
                            }
                            else
                            {
                                Console.WriteLine("이미 죽은 몬스터입니다. 다른 몬스터를 선택해 주세요");
                                AttackMenu();
                            }
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("MP가 부족합니다.");
                        AttackMenu();
                    }
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Random random = new Random();
                    if (player._currentmp >= 15)
                    {
                        Console.WriteLine($"{player._playerjobs.Skill2_Name} 사용!\n");
                        player._currentmp -= 15;
                        // 랜덤으로 2명의 적 공격
                        for (int i = 0; i < 2 && enemies.Count > 0; i++)
                        {
                            Enemy enemy = enemies[random.Next(enemies.Count)];
                            if (enemy.isDie)
                                continue;

                            int damage = (int)MathF.Round(1.5f * player._attack);
                            int preEnemyhp = enemy.HP;
                            enemy.HP = damage;
                            string isDieString = !enemy.isDie ? enemy.HP.ToString() : "Dead";
                            Console.WriteLine($"Lv.{enemy.level} {enemy.name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                            Console.WriteLine($"HP {preEnemyhp} ->{isDieString}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("MP가 부족합니다.");
                        AttackMenu();
                    }
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    AttackMenu();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
            IsEndBattle();
        }
        private void IsEndBattle()
        {
            Console.WriteLine("적의 공격 턴입니다.\n");
            Console.Write("Lv. " + player._level.ToString("D2"));
            Console.WriteLine($"   Chad.( {player._playerjobs._playerjob})");
            Console.WriteLine($"HP {playerpreBattleHp}-> {player.HP} \n");
            Console.WriteLine("0. 다음\n");

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
                ViewBattleVictoryResult();
            else
            {
                foreach (Enemy enemy in enemies) { }
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
    }
}