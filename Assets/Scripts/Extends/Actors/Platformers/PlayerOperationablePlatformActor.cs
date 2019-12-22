using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Actors.Platformers
{
    /// <summary>
    /// プレイヤークラス
    /// OperatuinablePlatformActorを継承
    /// Playerに必要な挙動と操作性向上の追実装をしている．
    /// </summary>
    public class PlayerOperationablePlatformActor : OperationablePlatformActor, IPlayer {

        [SerializeField] private GameObject deathEffectObject;
        public GameObject DeathEffectObject
            => this.deathEffectObject;

        public bool IsDeath { get; set; }

        /// <summary>
        /// isTrigger問わず相手と衝突した時にコール
        /// </summary>
        /// <param name="collision"></param>
        protected virtual void OnCollisionToOther(GameObject other)
        {
        }

        /// <summary>
        /// isTrigger問わず相手と衝突した時にコール
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnCollisionExitToOther(GameObject other)
        {
        }

        /// <summary>
        /// プレイヤーの死亡
        /// </summary>
        public virtual void Death()
        {
            this.IsDeath = true;
            Instantiate(this.DeathEffectObject, this.gameObject.transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject, Time.deltaTime*2f);
        }

        protected override void Update()
        {
            base.Update();
            ///プレイヤーのずり落ちを防止
    /*        if (!this.CurrentState.IsRunning && this.CurrentState.IsLanding)
            {
                this.Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                this.Rigidbody.freezeRotation = true;
            }
            else
            {
                this.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }*/
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionToOther(collision.gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.isTrigger)
            {
                this.OnCollisionToOther(collision.gameObject);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            this.OnCollisionExitToOther(collision.gameObject);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.isTrigger)
            {
                this.OnCollisionExitToOther(collision.gameObject);
            }
        }


        protected override void Start()
        {
            base.Start();
        }
    }
}
