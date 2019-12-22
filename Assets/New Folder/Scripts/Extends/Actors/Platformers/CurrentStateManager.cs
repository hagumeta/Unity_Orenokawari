using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Actors.Platformers
{

    /// <summary>
    /// 対象Actorの状態管理を行うクラス
    /// 簡単に言えば「地面に足が付いているか」，「空中にいるか」等の状態を保存している
    /// </summary>
    public class CurrentStateManager
    {
        //対象となるActor
        protected PlatformActor Actor;
        protected FootOfPlatformActor ActorFoot { get { return this.Actor.ActorFoot; } }

        /// <summary>
        /// コンストラクタ
        /// 対象Actorをセット
        /// </summary>
        /// <param name="actor"></param>
        public CurrentStateManager(PlatformActor actor)
        {
            this.Actor = actor;
        }


        public bool IsLanding { get; private set; }
        public bool IsLandingNow { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsFalling { get; private set; }
        public bool IsRunning { get; set; }   //走っているかどうかは知らんので外部からアクセスして更新できる
        public bool IsWallCatching { get; set; }//壁につかまっているかどうかは知らんので外部アクセスにより更新する


        /// <summary>
        /// 現在の状態を更新する
        /// </summary>
        public virtual void UpdateState()
        {

            if (this.ActorFoot != null)
            {
                if (!this.IsLanding && this.ActorFoot.IsLanding)
                {
                    this.IsLandingNow = true;
                }
                else
                {
                    this.IsLandingNow = false;
                }
                this.IsLanding = this.ActorFoot.IsLanding;
            }
            else
            {
                this.IsLanding = false;
            }


            if (!this.IsLanding)
            {//空中にいるとき
                if (this.Actor.VerticalSpeed > 0f)
                {   //上昇中
                    this.IsFalling = false;
                    this.IsJumping = true;
                }
                else
                {   //下降中
                    this.IsFalling = true;
                    this.IsJumping = false;
                }
            }
            else
            {//地面にいるとき
                this.IsFalling = false;
                this.IsJumping = false;
            }
        }
    }

}