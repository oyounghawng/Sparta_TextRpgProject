using Sparta_TextRpg.Scene;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Sparta_TextRpg
{
    internal class GameManager
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
        }

        public static GameManager Instance;
        public Player player;
        public DataManager datamanager;
        BaseScene[] scenes;
        BaseScene preScene;
        BaseScene curScene;

        public GameManager()
        {
            Instance = this;
            player = new Player();
            datamanager = new DataManager();

            int SceneNum = System.Enum.GetValues(typeof(SceneName)).Length;
            scenes = new BaseScene[SceneNum];

            scenes[(int)SceneName.LoginScene] = new LoginScene();
            scenes[(int)SceneName.SelectCharScene] = new SelectCharScene();
            scenes[(int)SceneName.StartScene] = new StartScene();
            scenes[(int)SceneName.StatusScene] = new StatusScene();
            scenes[(int)SceneName.BattleScene] = new BattleScene();
            scenes[(int)SceneName.StoreScene] = new StoreScene();
            scenes[(int)SceneName.QuestScene] = new QuestScene();
            scenes[(int)SceneName.InventoryScene] = new InventoryScene();
            scenes[(int)SceneName.DungeonScene] = new DungeonScene();
            scenes[(int)SceneName.RestScene] = new RestScene();

            ChangeScene(SceneName.LoginScene);

            Excute();
        }
        public void Excute()
        {
            while (true)
            {
                if (null != curScene)
                {
                    curScene.Excute();
                }
            }
        }
        public void LoadPreScene()
        {
            ChangeScene(preScene.sceneName);
        }
        public void ChangeScene(SceneName sceneName)
        {
            int idx = (int)sceneName;
            // 현재 씬이 있으면 이전 씬으로 설정 및 종료 처리
            if (null != curScene)
            {
                preScene = curScene;
                Console.Clear();
            }

            // 새로운 씬으로 변경 및 시작 처리
            curScene = scenes[idx];
            curScene.Enter();
        }
    }
}
