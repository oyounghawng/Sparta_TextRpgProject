using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class DohyunBattle : BaseScene
    {
        private List<Enemy> enemies;
        private Player player;
        private int playerpreBattleHp;
        private bool IsBattle = false;
        public override void Enter()
        {
            enemies = new List<Enemy>();
            player = GameManager.Instance.player;
            playerpreBattleHp = player._currnthp;
            Enemy enemy1 = new Enemy();
            //Enemy enemy2 = new Enemy();
            //Enemy enemy3 = new Enemy();
            enemies.Add(enemy1);
            //enemies.Add(enemy2);
            //enemies.Add(enemy3);

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
            Console.WriteLine($"   Chad.( {player._job})");
            Console.WriteLine($"HP {player._currnthp}/{player._maxhp}\n");
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 도망가기");
            //행동 선택
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    PlayerAttack();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.LoadPreScene();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    GameManager.Instance.ChangeScene(SceneName.StartScene);
                    break;
            }
        }
        private void ViewBattleVictoryResult()
        {
            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {enemies.Count}마리를 잡았습니다.\n");



            player._exp += enemies[0].exp;

            Console.WriteLine($"캐릭터 르탄이 경험치 {enemies[0].exp}를 획득했습니다");


            if (player._exp >= player._needlevelexp[player._level - 1])
            {
                while (player._exp >= player._needlevelexp[player._level - 1])
                {
                    player._exp -= player._needlevelexp[player._level - 1];
                    player._level++;
                    Console.WriteLine($"캐릭터 르탄이의 레벨이 {player._level}가 되었습니다");
                    IncreaseStats();
                    if (player._level - 1 >= player._needlevelexp.Length)
                    {
                        Console.WriteLine("더 이상 레벨업할 수 없습니다");
                        break;
                    }
                    else if (player._exp < player._needlevelexp[player._level - 1])
                    {
                        Console.WriteLine($"현재 경험치 {player._exp} / 필요 경험치 {player._needlevelexp[player._level - 1]}");
                    }
                }
            }


            Console.Write("Lv. " + player._level.ToString("D2"));
            Console.WriteLine($"   Chad.( {player._job})");
            Console.WriteLine($"HP {playerpreBattleHp}-> {player.HP} \n");
        }
        private void GameOver()
        {
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.Write("Lv. " + player._level.ToString("D2"));
            Console.WriteLine($"   Chad.( {player._job})");
            Console.WriteLine($"HP {player._currnthp} -> 0 \n");
            return; //다시할수도?
        }
        private void PlayerAttack()
        {
            Random random = new Random();
            float damage = player._attack;
            float offset = MathF.Round(damage * 0.1f);
            int offsetdamage = random.Next((int)(damage - offset), (int)(damage + offset + 1));
            int preEnemiseHp = enemies[0].HP;
            enemies[0].HP = offsetdamage;

            Console.WriteLine("Battle!!\n");
            Console.WriteLine(player._name + " 의 공격!");
            Console.WriteLine($"Lv.{enemies[0].level} {enemies[0].name} 을(를) 맞췄습니다. [데미지 : {offsetdamage}]\n");

            Console.WriteLine($"Lv.{enemies[0].level} {enemies[0].name}");
            string isDieString = !enemies[0].isDie ? enemies[0].HP.ToString() : "Dead";
            Console.WriteLine($"HP {preEnemiseHp} ->{isDieString} \n");

            Console.WriteLine("0. 다음");

            bool isEndBattle = true;
            //전투 종료 판단
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isDie)
                {
                    isEndBattle = false;
                }
                else
                {
                    isEndBattle = true;
                }


            }
            //모든 적의 사망확인
            if (isEndBattle)
                ViewBattleVictoryResult();
            else
            {
                foreach (Enemy enemy in enemies) { }
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        Console.Clear();
                        EnemyAttack();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

        }
        private void EnemyAttack()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"Lv.{enemies[0].level} {enemies[0].name} 의 공격!");
                Console.Write(player._name + "을(를) 맞췄습니다.");
                Console.WriteLine($"[데미지 : {enemies[0].atk}]");
                //체력 감소
                int preEnemyAttackHp = player.HP;
                player.HP = enemies[0].atk;
                Console.WriteLine($"HP {preEnemyAttackHp} ->{player.HP} \n");
                Console.WriteLine("0. 다음");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        Console.Clear();
                        PlayerAttack();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        // 스탯 증가 메서드
        private void IncreaseStats()
        {
            player._attack += 0.5f; // 공격력 0.5 증가
            player._defence += 1; // 방어력 1 증가
            Console.WriteLine($"레벨업! 현재 레벨: {player._level}, 공격력: {player._attack}, 방어력: {player._defence}");
        }





    }
}








//using System;

//namespace Sparta_TextRpg
//{
//    internal class DohyunBattle : BaseScene
//    {
//        Enemy enemy = new Enemy();
//        Player player = new Player(1, "플레이어 이름", PlayerJob.전사, 20, 5, 100, 0);
//        SkillManager skillManager = new SkillManager();

//        public override void Enter()
//        {
//            ViewMenu();
//        }

//        public override void ViewMenu()
//        {
//            bool isBattleContinuing = true;

//            while (isBattleContinuing)
//            {

//                // 몬스터 정보
//                DisplayEnemyStatus();

//                Console.WriteLine("1. 공격");
//                Console.WriteLine("2. 스킬");

//                string input = Console.ReadLine();
//                switch (input)
//                {
//                    case "1":
//                        // 공격 로직
//                        skillManager.Skill(player, enemy);


//                        if (enemy.hp <= 0)
//                        {
//                            Console.WriteLine($"{enemy.name}를 처치했습니다!");
//                            isBattleContinuing = false; // 전투 종료
//                        }
//                        break;
//                    case "2":
//                        // 스킬 사용 로직 (추가 구현 필요)
//                        Console.WriteLine("스킬을 사용합니다.");
//                        break;
//                    default:
//                        Console.WriteLine("잘못된 입력입니다.");
//                        break;
//                }
//            }
//        }

//        private void DisplayEnemyStatus()
//        {
//            Console.WriteLine();
//            Console.WriteLine($"이름: {enemy.name}");
//            Console.WriteLine($"체력: {enemy.hp}");
//            Console.WriteLine($"공격력: {enemy.atk}");
//            Console.WriteLine();
//        }
//    }
//}
