using System.Collections; 
using UnityEngine;
using Game.Stage.GameEvents;
using Extends.CameraControlls;

namespace Game.Stage.Actor
{
    [RequireComponent(typeof(CameraFocusThis))]
    public class Stickman_PlayerOperationablePlatformActor : PlayerOperationablePlatformActor, IPlayer
    {
        [SerializeField] private PlayerType _myPlayerType;
        [SerializeField] private PlayerDeathEvent playerDeathEvent;
        [SerializeField] private StageClearEvent stageClearEvent;

        public PlayerType playerType
            => this._myPlayerType;
        public Player player { get; private set; }
        private bool isMuteki = false;
        private SpriteRenderer sprite;

        override protected void Start()
        {
            base.Start();
            this.player = GameManager.PlayerCollection.GetPlayer(this.playerType);
            this.sprite = this.GetComponent<SpriteRenderer>();
            this.isMuteki = true;
            this.sprite.color = Color.gray;
            Invoke("NotMuteki", 0.25f);
        }

        private void NotMuteki()
        {
            this.isMuteki = false;
            this.sprite.color = Color.white;
            var a = this.GetComponent<Collider2D>();
            a.enabled = false;
            a.enabled = true;
        }

        /// <summary>
        /// isTrigger問わず相手と衝突した場合にコール
        /// </summary>
        /// <param name="collision"></param>
        protected override void OnCollisionToOther(GameObject other)
        {
            if (!this.IsFrozen) {
                if (!this.isMuteki) {
                    PlayerKiller a = other.GetComponent<PlayerKiller>();
                    if (a != null)
                    {
                        this.Death(a.deathType);
                        return;
                    }
                }

                IPlayerTouchGimick b = other.GetComponent<IPlayerTouchGimick>();
                if (b != null)
                {
                    b.OnPlayerTouched(this.transform);
                    return;
                }
            }
        }


        /// <summary>
        /// 死亡方法に応じた死体を生成する
        /// </summary>
        /// <param name="deathType"></param>
        public void CreateCorpse(DeathType deathType)
        {
            var _corpseObject = this.player.corpseCollection.GetCorpseObject(deathType);
            if (_corpseObject == null)
            {
                return;
            }

            var corpse = Instantiate(_corpseObject, this.transform.position, Quaternion.identity);
            corpse.transform.localScale = this.transform.localScale;
        }


        /// <summary>
        /// プレイヤーが死ぬ
        /// 死体生成とステージ管理者への報告
        /// </summary>
        /// <param name="deathType"></param>
        public void Death(DeathType deathType)
        {
            this.IsFrozen = true;
            this.playerDeathEvent.Raise(deathType);
            this.CreateCorpse(deathType);
            Destroy(this.gameObject);
        }


        /// <summary>
        /// 復活地点から復活した際にコールされる
        /// </summary>
        public void OnReborn()
        {
        }


        public void StageCleard()
        {
            this.stageClearEvent.Raise(this as IPlayer);
        }


        public void DoStageClearedAction()
        {
            StartCoroutine(this.CleardAction());
        }

        /// <summary>
        /// クリア時のアクション
        /// 飛び上がってピース
        /// </summary>
        /// <returns></returns>
        private IEnumerator CleardAction()
        {
            this.Operation.Lock();

            var cam = this.GetComponent<CameraFocusThis>();
            if (cam != null && cam.cameraController != null)
            {
                cam.cameraController.MoveLock = false;
                cam.cameraController.FocusOnObject(this.transform, 3, Vector2.zero);
            }

            yield return new WaitForSeconds(0.3f);
            while (!this.CurrentState.IsLanding)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            //ジャンプのつもり
            this.VerticalSpeed += this.ActionStatus.JumpSpeed * 1.5f;

            while (!this.CurrentState.IsLanding)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                if (this.CurrentState.IsFalling)
                {
                    this.VerticalSpeed -= 0.5f;
                }
            }
            yield return new WaitForSeconds(0.5f);

            this.Animator.SetTrigger("ClearAction");
        }


    }
}