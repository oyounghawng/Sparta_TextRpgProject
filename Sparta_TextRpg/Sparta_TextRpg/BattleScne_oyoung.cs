using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class BattleScne_oyoung : BaseScene
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
            Enemy enemy2 = new Enemy();
            //Enemy enemy3 = new Enemy();
            enemies.Add(enemy1);
            enemies.Add(enemy2);
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
                    AttackMenu();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    GameManager.Instance.LoadPreScene();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
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
            Console.WriteLine($"   Chad.( {player._job})");
            Console.WriteLine($"HP {player._currnthp}/{player._maxhp}\n");

            for(int i = 0; i< enemies.Count; i++)
            {
                Console.WriteLine($"{i+1}. 공격");
            }
            Console.WriteLine("0. 도망가기");
            var key = Console.ReadKey(true).Key;
            if (key >= ConsoleKey.D1 && key <=ConsoleKey.D1 + enemies.Count)
            {
                Console.Clear();
                int idx = (int)(key - 49);
                PlayerAttack(idx);
            }
            else if (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad1 + enemies.Count)
            {
                Console.Clear();
                int idx = (int)(key - 97);
                PlayerAttack(idx);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                ViewMenu();
            }

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
        private void PlayerAttack(int idx)
        {
            Random random = new Random();
            float damage = player._attack;
            float offset = MathF.Round(damage * 0.1f);
            int offsetdamage = random.Next((int)(damage - offset), (int)(damage + offset + 1));
            int preEnemiseHp = enemies[idx].HP;
            enemies[0].HP = offsetdamage;

            Console.WriteLine("Battle!!\n");
            Console.WriteLine(player._name + " 의 공격!");
            Console.WriteLine($"Lv.{enemies[idx].level} {enemies[idx].name} 을(를) 맞췄습니다. [데미지 : {offsetdamage}]\n");

            Console.WriteLine($"Lv.{enemies[idx].level} {enemies[idx].name}");
            string isDieString = !enemies[idx].isDie ? enemies[idx].HP.ToString() : "Dead";
            Console.WriteLine($"HP {preEnemiseHp} ->{isDieString} \n");
            Console.WriteLine("적의 공격 턴입니다.\n");
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
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.Clear();
                Console.WriteLine("적의 공격 턴입니다.");
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"{i+1}번쨰 Lv.{enemies[i].level} {enemies[i].name} 의 공격!");
                Console.Write(player._name + "을(를) 맞췄습니다.");
                Console.WriteLine($"[데미지 : {enemies[i].atk}]");
                //체력 감소
                int preEnemyAttackHp = player.HP;
                player.HP = enemies[i].atk;
                Console.WriteLine($"HP {preEnemyAttackHp} ->{player.HP} \n");
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
            switch (key2)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    AttackMenu();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }
}