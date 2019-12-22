using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Extends.Actors.Platformers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformActor : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem RunEffect;
        [SerializeField]
        protected ParticleSystem LandEffect;
        [SerializeField]
        protected ParticleSystem WallCatchEffect;

        public bool isFrozen = false;
        public bool IsFrozen
        {
            get => this.isFrozen;
            set
            {
                this.isFrozen = value;
            }
        }


        //メンバ変数
        public Animator Animator;
        protected Rigidbody2D Rigidbody;
        protected CurrentStateManager CurrentState;
        protected AnimationManager Animation;
        /// <summary>
        /// ActorFootについて
        /// このスクリプトをつけたGameObject下に"Foot"という名のGmaeObjectがあり，
        /// "Foot"についているFootOfActorから参照している
        /// </summary> 
        public FootOfPlatformActor ActorFoot;



        //自身のRigidbodyの速度プロパティ
        public float HorizontalSpeed
        {   //水平方向(x)
            set { this.Rigidbody.velocity = new Vector2(value, this.VerticalSpeed); }
            get { return this.Rigidbody.velocity.x; }
        }
        public float VerticalSpeed
        {   //垂直方向(y)
            set { this.Rigidbody.velocity = new Vector2(this.HorizontalSpeed, value); }
            get { return this.Rigidbody.velocity.y; }
        }

        //水平方向について，Actorが向いている方向　のプロパティ(1:右，-1:左)
        public int FacingDirectionHorizontal
        {
            get
            {
                if (this.transform.localScale.x > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                int axis = Mathf.Clamp(value, -1, 1);
                if (axis != 0)
                {
                    this.transform.localScale = new Vector3(axis * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                }
            }
        }


        /// <summary>
        /// 初期化
        /// 自身のパラメータをそれぞれ取得/初期化
        /// </summary>
        protected virtual void Start()
        {
            this.Rigidbody = this.GetComponent<Rigidbody2D>();
            this.Animator = this.GetComponent<Animator>();

            //自身のgameObjectの子からFootOfActorを取得する
            this.ActorFoot = this.GetComponentInChildren<FootOfPlatformActor>();

            this.Animation = new AnimationManager(this);
            this.CurrentState = new CurrentStateManager(this);
        }


        /// <summary>
        /// エフェクトを発生させる
        /// </summary>
        virtual public void PlayEffect(ParticleSystem particleSystem)
        {
            if (particleSystem != null)
            {
                if (!particleSystem.isPlaying)
                {
                    particleSystem.Play();
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            //走るエフェクト
            if (this.CurrentState.IsRunning && this.CurrentState.IsLanding)
            {
                this.RunEffect.Emit((int)Random.RandomRange(0, 2));
            }
            //壁エフェクト
            if (this.CurrentState.IsWallCatching)
            {
                this.PlayEffect(this.WallCatchEffect);
            }
        }


        /// <summary>
        /// 各フレーム更新
        /// AnimationとCurrentStateを更新する
        /// </summary>
        protected virtual void Update()
        {
            if (this.IsFrozen)
            {   //frozen中なら動作停止
                this.Rigidbody.velocity = Vector2.zero;
                return;
            }
            else
            {
                this.CurrentState.UpdateState();
                this.Animation.UpdateAnimation();

                //着地エフェクト
                if (this.CurrentState.IsLandingNow)
                {
                    this.PlayEffect(this.LandEffect);
                }
            }
        }



        //===========================================================================================================





        //===========================================================================================================

        /// <summary>
        /// 対象ActorのAnimation管理を行うクラス
        /// とは言っているもののAnimatorを参照する
        /// 現在のActorの状態CurrentStateに合わせてAnimatorに反映させる
        /// </summary>
        public class AnimationManager
        {
            //対象となるActor
            protected PlatformActor Actor;

            //それぞれ対象Actorからパラメータ取得するプロパティ
            protected Animator Animator { get { return this.Actor.Animator; } }
            protected CurrentStateManager CurrentState { get { return this.Actor.CurrentState; } }

            /// <summary>
            /// コンストラクタ
            /// 対象Actorをセット
            /// </summary>
            /// <param name="actor"></param>
            public AnimationManager(PlatformActor actor)
            {
                this.Actor = actor;
            }

            /// <summary>
            /// 対象ActorのAnimatorに現在のActorの状態を反映させる
            /// </summary>
            public virtual void UpdateAnimation()
            {
                if (this.Animator != null)
                {
                    this.Animator.SetBool("IsOnGround", this.CurrentState.IsLanding);
                    this.Animator.SetBool("IsRunning", this.CurrentState.IsRunning);
                    this.Animator.SetBool("IsJumping", this.CurrentState.IsJumping);
                    this.Animator.SetBool("IsFalling", this.CurrentState.IsFalling);
                    this.Animator.SetBool("IsWallCatching", this.CurrentState.IsWallCatching);
                }
            }
        }
    }
}
