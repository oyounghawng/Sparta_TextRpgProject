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
        public List<Quest> Quests { get; private set; }
        public DataManager()
        {
            Items = new List<Item>();
            Enemys = new List<Enemy>();
            Quests = new List<Quest>();

            Init();
        }
        private void Init()
        {   
            #region Gear
            Items.Add(new Item("나무칼", ItemType.WEAPON, ItemRating.RARE, 10, 0, "나무로 만든칼", 100));
            Items.Add(new Item("철칼", ItemType.WEAPON, ItemRating.UNIQUE, 20, 0, "철로 만든칼", 200));
            Items.Add(new Item("다이아칼", ItemType.WEAPON, ItemRating.LEGEND, 30, 0, "다이아로 만든칼", 300));
            Items.Add(new Item("나무투구", ItemType.HELMET, ItemRating.RARE, 1, 0, "나무로 만든투구", 100));
            Items.Add(new Item("철투구", ItemType.HELMET, ItemRating.UNIQUE, 3, 0, "철로 만든투구", 200));
            Items.Add(new Item("다이아투구", ItemType.HELMET, ItemRating.LEGEND, 5, 0, "다이아로 만든투구", 300));
            Items.Add(new Item("나무갑옷", ItemType.ARMOR, ItemRating.RARE, 1, 0, "나무로 만든갑옷", 100));
            Items.Add(new Item("철갑옷", ItemType.ARMOR, ItemRating.UNIQUE, 3, 0, "철로 만든갑옷", 200));
            Items.Add(new Item("다이아갑옷", ItemType.ARMOR, ItemRating.LEGEND, 5, 0, "다이아로 만든갑옷", 300));
            Items.Add(new Item("나무신발", ItemType.SHOES, ItemRating.RARE, 1, 0, "나무로 만든신발", 100));
            Items.Add(new Item("철신발", ItemType.SHOES, ItemRating.UNIQUE, 3, 0, "철로 만든신발", 200));
            Items.Add(new Item("다이아신발", ItemType.SHOES, ItemRating.LEGEND, 5, 0, "다이아로 만든신발", 300));
            #endregion

            #region Potion
            Items.Add(new Item("하급 체력 포션", ItemType.POTION, ItemRating.RARE, 10, 0,"작은 회복", 50));
            Items.Add(new Item("중급 체력 포션", ItemType.POTION, ItemRating.UNIQUE, 20, 0, "중간 회복", 150));
            Items.Add(new Item("상급 체력 포션", ItemType.POTION, ItemRating.LEGEND, 30, 0, "큰 회복", 250));
            Items.Add(new Item("하급 마나 포션", ItemType.POTION, ItemRating.RARE, 10, 0, "작은 회복", 50));
            Items.Add(new Item("중급 마나 포션", ItemType.POTION, ItemRating.UNIQUE, 20, 0, "중간 회복", 150));
            Items.Add(new Item("상급 마나 포션", ItemType.POTION, ItemRating.LEGEND, 30, 0, "큰 회복", 250));
            #endregion

            #region Enemy
            Enemys.Add(new Enemy("슬라임"));
            Enemys.Add(new Enemy("스켈레톤"));
            Enemys.Add(new Enemy("오크"));
            #endregion

            #region Quest
            Quests.Add(new Quest("마을을 위협하는 슬라임 처치", "이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\n" +
                          "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                          "모험가인 자네가 좀 처치해주게!", "슬라임 5 마리 처치", 500, 5, Enemys[0]));
            Quests.Add(new Quest("마을을 위협하는 스켈레톤 처치", "이봐! 마을 근처에 스켈레톤 너무 많아졌다고 생각하지 않나?\n" +
                           "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                           "모험가인 자네가 좀 처치해주게!", "스켈레톤 5 마리 처치", 500, 5, Enemys[1]));
            Quests.Add(new Quest("마을을 위협하는 슬라임 처치", "이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\n" +
                            "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                           "모험가인 자네가 좀 처치해주게!", "슬라임 5 마리 처치", 500, 5, Enemys[2]));

            #endregion

        }
    }
}


