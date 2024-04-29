using System.Numerics;

namespace Sparta_TextRpg
{
    internal class GameManager
    {

        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.ChangeScene(SceneName.StartScene);
        }

        public static GameManager Instance;
        public Player player = new Player(1, "르탄", PlayerJob.전사, 50, 5, 100, 1500);

        BaseScene[] scenes;
        BaseScene preScene;
        BaseScene curScene;

        public GameManager()
        {
            Instance = this;

            scenes = new BaseScene[4];

            scenes[0] = new StartScene();
            scenes[1] = new StatusScene();
            scenes[2] = new InventoryScene();
            scenes[3] = new StoreScene();


            Instance = this;
            int SceneNum = System.Enum.GetValues(typeof(SceneName)).Length;
            scenes = new BaseScene[SceneNum];
            scenes[(int)SceneName.StartScene] = new StartScene();
            scenes[(int)SceneName.StatusScene] = new StatusScene();
            scenes[(int)SceneName.BattleScene] = new DohyunBattle();
            ChangeScene(SceneName.StartScene);
            Excute();


            ChangeScene(0);
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
