using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Sparta_TextRpg
{
    internal class DataManager
    {
        public static DataManager Instance = new DataManager();
        public static List<Item> Items { get; private set; }
        public static List<Enemy> Enemys { get; private set;}

        static DataManager()
        {
            Items = new List<Item>
            {
                new Item("대검", ItemType.WEAPON, 3, "쇳덩어리다.", 1000),
                new Item("나무 활", ItemType.WEAPON, 3, "나무다.", 1000),
                new Item("크리스탈 지팡이", ItemType.WEAPON, 3, "비싸보인다.", 1000),

                new Item("하급 포션(HP)", ItemType.POTION, 10, "작은 회복", 50),
                //new Item("중급 포션(HP)", ItemType.POTION, 20, "중간 회복", 150),
                new Item("상급 포션(HP)", ItemType.POTION, 30, "큰 회복", 250),
                new Item("하급 포션(MP)", ItemType.POTION, 10, "작은 회복", 50),
                //new Item("중급 포션(MP)", ItemType.POTION, 20, "작은 회복", 150),
                new Item("상급 포션(MP)", ItemType.POTION, 30, "큰 회복", 250)
            };

            Enemys = new List<Enemy>();
            Enemys.Add(new Enemy());
        }




    }
}


