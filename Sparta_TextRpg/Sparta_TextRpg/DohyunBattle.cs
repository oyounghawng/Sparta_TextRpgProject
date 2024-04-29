using System;

namespace Sparta_TextRpg
{
    internal class DohyunBattle : BaseScene
    {
        Enemy enemy = new Enemy();
        Player player = new Player(1, "플레이어 이름", PlayerJob.전사, 20, 5, 100, 0);
        SkillManager skillManager = new SkillManager();

        public override void Enter()
        {
            ViewMenu();
        }

        public override void ViewMenu()
        {
            bool isBattleContinuing = true;

            while (isBattleContinuing)
            {

                // 몬스터 정보
                DisplayEnemyStatus();

                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        // 공격 로직
                        skillManager.Skill(player, enemy);


                        if (enemy.hp <= 0)
                        {
                            Console.WriteLine($"{enemy.name}를 처치했습니다!");
                            isBattleContinuing = false; // 전투 종료
                        }
                        break;
                    case "2":
                        // 스킬 사용 로직 (추가 구현 필요)
                        Console.WriteLine("스킬을 사용합니다.");
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        private void DisplayEnemyStatus()
        {
            Console.WriteLine();
            Console.WriteLine($"이름: {enemy.name}");
            Console.WriteLine($"체력: {enemy.hp}");
            Console.WriteLine($"공격력: {enemy.atk}");
            Console.WriteLine();
        }
    }
}
