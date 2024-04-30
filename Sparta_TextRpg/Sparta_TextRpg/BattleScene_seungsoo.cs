using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class BattleScne_seungsoo : BaseScene
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
            // Enemy enemy2 = new Enemy();
            //Enemy enemy3 = new Enemy();
            enemies.Add(enemy1);
            // enemies.Add(enemy2);
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
            Console.WriteLine("2. 스킬");
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
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    PlayerSkill();
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

        private void PlayerSkill()
        {
            Console.WriteLine("Battle!!\n");
        }

        private void ViewBattleVictoryResult()
        {
            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {enemies.Count}마리를 잡았습니다.\n");
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
            int preEnemiseHp = enemies[0].HP;
            if (critic == true)
            {
                offsetdamage = (int)MathF.Round(1.6f * (offsetdamage));


            }

            enemies[0].HP = offsetdamage;
            Console.WriteLine("Battle!!\n");
            Console.WriteLine(player._name + " 의 공격!");
            Console.WriteLine($"Lv.{enemies[0].level} {enemies[0].name} 을(를) 맞췄습니다. [데미지 : {offsetdamage}]\n");
            if (critic == true)
            {
                Console.WriteLine("치명타 공격!");
            }
            Console.WriteLine($"Lv.{enemies[0].level} {enemies[0].name}");
            string isDieString = !enemies[0].isDie ? enemies[0].HP.ToString() : "Dead";
            Console.WriteLine($"HP {preEnemiseHp} ->{isDieString} \n");

            Console.WriteLine("0. 다음");

            bool isEndBattle = true;
            //전투 종료 판단
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isDie)
                    isEndBattle = false;
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
            bool avoidance = false;
            Random random = new Random();
            int eatk = enemies[0].atk;
            for (int i = 0; i < enemies.Count; i++)
            {
                int avoid = random.Next(1, 101);// 회피
                if (avoid <= 10)
                {
                    avoidance = true;
                }
                else
                {
                    avoidance = false;
                }

                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"Lv.{enemies[0].level} {enemies[0].name} 의 공격!");
                Console.Write(player._name + "을(를) 공격하였습니다.");

                //체력 감소
                int preEnemyAttackHp = player.HP;
                if (avoidance == true)
                {
                    eatk = 0 * (enemies[0].atk);
                    Console.WriteLine("회피하였습니다.");
                    avoidance = false;
                }

                player.HP = eatk;
                Console.WriteLine($"[데미지 : {eatk}]");
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
    }
}