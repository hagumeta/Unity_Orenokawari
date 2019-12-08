using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Game.Data.Dynamic;
using Game.Data.Static;

using System.Linq;
using UnityEngine.SceneManagement;
using Game.StageSelect;

namespace Game.Stage.Manager
{
    [RequireComponent(typeof(CoinScoreManager))]
    [RequireComponent(typeof(PlayerDeathManager))]
    [RequireComponent(typeof(PlayerStageClearManager))]

    public class StageManager : SingletonMonoBehaviourFast<StageManager>
    {

        [SerializeField] private SceneObject returnToScene;
        [SerializeField] private StageData stageData;
        [SerializeField] private WorldData worldData;

        public static DeathCounts MyDeathCounts
            => Instance.myDeathCounts;
        public static CoinScore MyCoinScore
            => Instance.myCoinScore;

        public StageData StageData
            => this.stageData;
        public WorldData WorldData
            => this.worldData;

        private int StageID
            => this.StageData.StageId;
        private DeathCounts myDeathCounts
            => this.playerDeathManager.DeathCounts;
        private CoinScore myCoinScore
            => this.coinScoreManager.CoinScore;

        private CoinScoreManager coinScoreManager;
        private PlayerDeathManager playerDeathManager;
        private PlayerStageClearManager playerStageClearManager;



        public static void StageReset()
        {
            SceneTransitionManager.GotoScene(Instance.stageData.StageScene, 0.2f, 0.15f);
        }

        public static void ReturnToSelectScene()
        {
            GameManager.MoveQueuedScene();
        }

        protected override void Init()
        {
            this.playerDeathManager = this.GetComponent<PlayerDeathManager>();
            this.coinScoreManager = this.GetComponent<CoinScoreManager>();
            this.playerStageClearManager = this.GetComponent<PlayerStageClearManager>();

            if (GameManager.NowStageInformation == null)
            {
                var index = this.worldData.GetStageIndex(this.stageData.StageId);
                var information = new StageInformation(this.worldData.WorldId, index);
                var request = new MoveStageRequest(information, this.returnToScene);
                GameManager.ImplyRequest(request);
            }
            this.DataInit(GameManager.NowStageInformation);
        }


        /// <summary>
        /// データの初期化をする．
        /// 現在のステージ情報獲得のため，MoveStageRequestが必要．
        /// </summary>
        /// <param name="stageData"></param>
        private void DataInit(MoveStageRequest request)
        {
            this.stageData = request.StageInformation.StageData;
            this.worldData = request.StageInformation.WorldData;
            this.returnToScene = request.ReturnToScene;

            this.Reset();
        }


        /// <summary>
        /// プレイヤーの初期位置とデス数を初期化する
        /// </summary>
        private void Reset()
        {
            this.coinScoreManager.Init(this.StageID);
            this.playerDeathManager.Init(this.StageID);
            this.playerStageClearManager.Init(this.StageID);

            this.playerDeathManager.StartPlayer();
        }


        /// <summary>
        /// StageIDに対応したStageSaveへスコアを保存する
        /// </summary>
        public void Store()
        {
            var save = GameManager.StageSaveData.Get(this.StageID);
            save.SaveWithCleared(this.myDeathCounts, this.myCoinScore);
        }
    }


    /// <summary>
    /// ステージシーンに移行した際に呼ばれるメソッドを集めておくクラス
    /// </summary>
    public class StageInitializer
    {
        private const string InitializeSceneName = "StageTemplate";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RuntimeInitializeApplication()
        {
            var activeScene = SceneManager.GetActiveScene().name;

            if (activeScene.Contains("Stage_")) {
                if (!SceneManager.GetSceneByName(InitializeSceneName).IsValid())
                {
                    SceneManager.LoadScene(InitializeSceneName, LoadSceneMode.Additive);
                }
            }
        }
    }
}