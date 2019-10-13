using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Data.Stage;
using Game.Stage;
using InputUtil;
using Data.Scene;

namespace Game
{
    public class GameManager : SingletonMonoBehaviourFast<GameManager>
    {
        private bool isInited;
        private StageSaveDatas stageSaveDatas;
        [SerializeField] private DeathIconCollection deathCollection;
        [SerializeField] private PlayerCollection playerCollection;
        [SerializeField] private GameScenes gameScenes;

        private StageDataController stageDataController;
        private WorldDataController worldDataController;
        private MoveStageRequest nowStageRequest;
        private Queue<SceneObject> sceneQueue;

        private void Start()
        {
            this.Init();
            DontDestroyOnLoad(this.gameObject);
        }


        protected override void Init()
        {
            if (!this.isInited) {
                Instance = this;
                this.stageDataController = new StageDataController();
                this.worldDataController = new WorldDataController();
                this.stageSaveDatas = new StageSaveDatas();
                this.isInited = true;
                this.sceneQueue = new Queue<SceneObject>();
                SceneManager.activeSceneChanged += this.OnActiveSceneChanged;
                new ButtonOperation();
            }
        }



        public static StageSaveDatas StageSaveData
            => Instance.stageSaveDatas;
        public static DeathIconCollection DeathIconCollection
            => Instance.deathCollection;
        public static PlayerCollection PlayerCollection
            => Instance.playerCollection;
        public static StageDataController StageData
            => Instance.stageDataController;
        public static WorldDataController WorldData
            => Instance.worldDataController;
        public static MoveStageRequest NowStageInformation
            => Instance.nowStageRequest;

        public static void MoveStageScene(MoveStageRequest request)
        {
            Instance.moveStageScene(request);
        }
        public static void MoveQueuedScene()
        {
            Instance.moveQueuedScene();
        }

        private void moveStageScene(MoveStageRequest request)
        {
            this.nowStageRequest = request;
            this.sceneQueue.Enqueue(request.ReturnToScene);
            SceneTransitionManager.GotoScene(request.StageInformation.StageScene);
        }
        public static void ImplyRequest(MoveStageRequest request)
        {
            Instance.nowStageRequest = request;
            Instance.sceneQueue.Enqueue(request.ReturnToScene);
        }

        private void moveQueuedScene()
        {
            SceneTransitionManager.GotoScene(this.sceneQueue.Dequeue());
        }


        public static void MoveTitleScene()
        {
            SceneTransitionManager.GotoScene(Instance.gameScenes.TitleScene);
        }
        public static void MoveStageSelectScene()
        {
            SceneTransitionManager.GotoScene(Instance.gameScenes.StageSelectScene);
        }
        public static void MoveFreeStageSelectScene()
        {
            SceneTransitionManager.GotoScene(Instance.gameScenes.FreeStageSelectScene);
        }






        private const string InitializeSceneName = "StageTemplate";

        private void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name.Contains("Stage_"))
            {
                if (!SceneManager.GetSceneByName(InitializeSceneName).IsValid())
                {
                    SceneManager.LoadScene(InitializeSceneName, LoadSceneMode.Additive);
                }
            }

        }

    }
}