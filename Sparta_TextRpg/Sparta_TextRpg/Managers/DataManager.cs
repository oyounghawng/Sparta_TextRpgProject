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
        public List<Item> Items { get; private set; }
        public List<Enemy> Enemys { get; private set;}

        public DataManager()
        {
            Items = new List<Item>();
            Enemys = new List<Enemy>();
            

            Init();
        }
        private void Init()
        {
            Item item1 = new Item("나무칼", ItemType.WEAPON, ItemRating.COMMON , 5, "나무로 만든칼", 500);
            Item item2 = new Item("돌칼", ItemType.WEAPON, ItemRating.RARE, 5, "돌로 만든칼", 500);
            Item item3 = new Item("구리칼", ItemType.WEAPON, ItemRating.UNIQUE, 5, "구리로 만든칼", 500);
            Item item4 = new Item("다이아칼", ItemType.WEAPON, ItemRating.LEGEND, 5, "다이아로 만든칼", 500);
            Items.Add(item1);
            Items.Add(item2);
            Items.Add(item3);
            Items.Add(item4);

            Items.Add(new Item("하급 체력 포션", ItemType.POTION, ItemRating.COMMON, 10, "작은 회복", 50));
            Items.Add(new Item("중급 체력 포션", ItemType.POTION, ItemRating.RARE, 20, "중간 회복", 150));
            Items.Add(new Item("상급 체력 포션", ItemType.POTION, ItemRating.UNIQUE, 30, "큰 회복", 250));
            Items.Add(new Item("하급 마나 포션", ItemType.POTION, ItemRating.COMMON, 10, "작은 회복", 50));
            Items.Add(new Item("중급 마나 포션", ItemType.POTION, ItemRating.RARE, 20, "중간 회복", 150));
            Items.Add(new Item("상급 마나 포션", ItemType.POTION, ItemRating.UNIQUE, 30, "큰 회복", 250));

            Enemys.Add(new Enemy("슬라임"));
            Enemys.Add(new Enemy("스켈레톤"));
            Enemys.Add(new Enemy("오크"));
        }
    }
}


