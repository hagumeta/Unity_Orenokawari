using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Stage;
using System.Linq;
using UnityEngine.SceneManagement;
using Game.StageSelect;

namespace Game.Stage.Manager
{
    [RequireComponent(typeof(CoinScoreManager))]
    [RequireComponent(typeof(PlayerDeathManager))]

    public class StageManager : SingletonMonoBehaviourFast<StageManager>
    {

        [SerializeField] private SceneObject returnToScene;
        [SerializeField] private StageData stageData;
        [SerializeField] private WorldData worldData;

        private CoinScoreManager coinScoreManager;
        private PlayerDeathManager playerDeathManager;



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



        public static DeathCounts MyDeathCounts
            => Instance.myDeathCounts;
        public static CoinScore MyCoinScore
            => Instance.myCoinScore;

        /*
        //
                public static DeathCounts MyDeathCounts
                    => Instance.deathCounts;
                public static CoinScore MyCoinScore
                    => Instance.coinScore;

                private CoinScore coinScore;



        //
                private DeathCounts deathCounts;
                public static void PlayerDeath(DeathType deathType)
                {
                    Instance.DeathCountUp(deathType);
                    Instance.Invoke("RebornPlayer", 1f);
                }
                /// <summary>
                /// 対応する死亡タイプのデス数をカウントアップする
                /// </summary>
                /// <param name="deathType"></param>
                private void DeathCountUp(DeathType deathType)
                {
                    Instance.deathCounts.CountUp(deathType);
                    GameEventManager.PlayerDeathEvent.Raise(deathType);
        //            GameEventManager.DeathCountChangedEvent.Raise();
                }
        */


        public static void StageClear()
        {
            Instance.Store();
        }
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

            /*
            var a = FindObjectOfType<PlayerStartPoint>();
            var player = this.CreatePlayer(a.playerType, a.transform.position);
            
            a.OnPlayerTouched(player);
            */

            this.playerDeathManager.StartPlayer();
        }


        /// <summary>
        /// StageIDに対応したStageSaveへスコアを保存する
        /// </summary>
        private void Store()
        {
            var save = GameManager.StageSaveData.Get(this.StageID);
            save.SaveWithCleared(this.myDeathCounts, this.myCoinScore);
        }
/*        
        /// <summary>
        /// プレイヤーの最後に到達したチェックポイントへプレイヤーを生成する
        /// </summary>
        private void RebornPlayer()
        {
            var checkPoint = GetPlayerCheckPoint();
            this.CreatePlayer(checkPoint.playerType, checkPoint.transform.position);
        }

        /// <summary>
        /// プレイヤーを生成する
        /// </summary>
        /// <param name="player"></param>
        /// <param name="position"></param>
        private Transform CreatePlayer(Player player, Vector3 position)
        {
            var obj = Instantiate(player.PlayerObject);
            obj.transform.position = position;
            return obj.transform;
        }
        private Transform CreatePlayer(PlayerType playerType, Vector3 position)
        {
            return this.CreatePlayer(GameManager.PlayerCollection.GetPlayer(playerType), position);
        }


        /// <summary>
        /// プレイヤーが最後に通ったチェックポイントを探して取得する
        /// </summary>
        /// <returns></returns>
        private CheckPoint GetPlayerCheckPoint()
        {
            CheckPoint latestCheckPoint
                = GameObject.FindObjectsOfType<CheckPoint>()
                .Single(a => a.IsLatest);

            return latestCheckPoint;
        }

    */
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