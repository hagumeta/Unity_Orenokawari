using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.GameEvents;
using System.Linq;
using Game.Data.Dynamic;


namespace Game.Stage.Manager
{
    public class PlayerDeathManager : MonoBehaviour, IPlayerDeathEventListener, IManager
    {
        [SerializeField] private float playerRebornTime = 1f;

        public DeathCounts DeathCounts { get; private set; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="stageID"></param>
        public void Init(int stageID)
        {
            this.DeathCounts = new DeathCounts(stageID);
        }

        /// <summary>
        /// PlayerDeathEventコール時の処理
        /// </summary>
        public void OnEventRaised() { }
        public void OnEventRaised(DeathType deathType)
        {
            this.DeathCounts.CountUp(deathType);
            this.Invoke("RebornPlayer", this.playerRebornTime);
        }

        /// <summary>
        /// プレイヤー初期値にプレイヤーを新しく生成する
        /// </summary>
        public void StartPlayer()
        {
            var a = FindObjectOfType<PlayerStartPoint>();
            var player = this.CreatePlayer(a.playerType, a.transform.position);
            a.OnPlayerTouched(player);
        }



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

        /// <summary>
        /// プレイヤーを生成する
        /// </summary>
        /// <param name="playerType"></param>
        /// <param name="position"></param>
        /// <returns></returns>
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

    }
}