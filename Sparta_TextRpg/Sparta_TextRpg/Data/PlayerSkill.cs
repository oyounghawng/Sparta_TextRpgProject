using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg.Data
{

    public class PlayerSkill
    {
        private List<Enemy> enemies;
        private Player player;
        public PlayerSkill()
        {
            enemies = new List<Enemy>();
            player = GameManager.Instance.player;
            //playerpreBattleHp = player._currenthp;
            Enemy enemy1 = new Enemy("스켈레톤");
            Enemy enemy2 = new Enemy("슬라임");
            enemies.Add(enemy1);
            enemies.Add(enemy2);
            Console.WriteLine("Battle!!\n");
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($" Lv.{enemies[i].level} {enemies[i].name} HP{enemies[i].hp}");
            }

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv. {player._level}  Chad ({player._job})");
            Console.WriteLine($"HP {player._currenthp}/{player._maxhp}");
            Console.WriteLine($"MP {player._currentmp}/{player._maxmp}\n");
            Console.WriteLine("1. 알파 스트라이크 - MP 10");
            Console.WriteLine("   공격력 * 2 로 하나의 적을 공격합니다.");
            Console.WriteLine("2. 더블 스트라이크 - MP 15");
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
                        player._currentmp -= 10;
                        if (enemies.Count > 0)
                        {
                            int damage = (int)MathF.Round(2 * player._attack);
                            enemies[0].HP = damage;
                            Console.WriteLine($"알파 스트라이크 사용!{damage}를 입혔습니다!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("MP가 부족합니다.");
                    }
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    Random random = new Random();
                    if (player._currentmp >= 15)
                    {
                        // 더블 스트라이크
                        player._currentmp -= 15;
                        // 랜덤으로 2명의 적 공격
                        for (int i = 0; i < 2 && enemies.Count > 0; i++)
                        {
                            Enemy enemy = enemies[random.Next(enemies.Count)]; 
                            int damage = (int)MathF.Round(1.5f * player._attack);
                            enemy.HP = damage; 
                            Console.WriteLine($"더블 스트라이크 사용! {damage} 데미지를 입혔습니다!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("MP가 부족합니다.");
                    }
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.Clear();
                    // 취소
                    return; // 메인 메뉴로 복귀
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

        }
    }
}
