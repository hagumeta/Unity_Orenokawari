using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extends.Physics2DExtension;

namespace Extends.Actors.Platformers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class OperationablePlatformActor : PlatformActor
    {

        //メンバ変数<インスペクタ表示>
        public ButtonOperation Operation;
        public PlatformerActionStatus ActionStatus;
        public LayerMask layerMask;

        //入力を受けた結果，移動する方向(左：-1  右:1  動かない:0)
        private float moveHorizontalAxis;

        private bool IsActionLocked = false;
        private float wallCatchedTime = 0;

        private bool jumpPressedFlag = false;

        /// <summary>
        /// 毎フレームの更新
        /// 入力を受け付ける
        /// </summary>
        protected override void Update()
        {
            if (!this.IsFrozen)
            {
                base.Update();
                this.CurrentState.IsWallCatching = false;

                //this.moveHorizontalAxisの更新(ActionLockが掛かっているなら更新されない)
                if (!this.IsActionLocked)
                {
                    //横方向の入力から進むべき方向を取得する
                    if (this.Operation.Horizontal.IsPressing)
                    {
                        //入力アリなら入力方向に設定(1 or -1 or 0)
                        RaycastHit2D[] hits = new RaycastHit2D[6];
                        for (int i = 0; i < hits.Length; i++)
                        {
                            float y = (i - 2.5f) * 0.15f * this.transform.localScale.x;
                            Vector2 startPos = this.transform.position + new Vector3(0, y);
                            hits[i] = RaycastExtension.RaycastAndDraw(startPos, new Vector2(this.Operation.Horizontal.AxisRaw, 0), 0.23f, this.layerMask);
                        }
                        if (hits.Count(a => a.collider != null && !a.collider.isTrigger) <= 0)
                        {
                            this.moveHorizontalAxis = this.Operation.Horizontal.AxisRaw;
                        }
                        else
                        {
                            if (this.ActionStatus.CanWallJump && this.CurrentState.IsFalling && !this.CurrentState.IsLanding)
                            {
                                this.CurrentState.IsWallCatching = true;
                                this.FacingDirectionHorizontal = (int)this.Operation.Horizontal.AxisRaw;
                                this.wallCatchedTime = Time.time;
                            }
                            this.moveHorizontalAxis = 0f;
                        }
                    }
                    else
                    {   //入力がないなら0(動かない)
                        this.moveHorizontalAxis = 0f;
                    }

                    //CurrentStateの更新
                    this.CurrentState.IsRunning = this.moveHorizontalAxis != 0;
                    if (this.CurrentState.IsLanding)
                    {
                        this.FacingDirectionHorizontal = (int)this.moveHorizontalAxis;
                        this.CurrentState.IsWallCatching = false; //強制壁つかまり解除
                        this.wallCatchedTime = 0;
                    }
                    if (!this.CurrentState.IsWallCatching)
                    {
                        if (!(Time.time - this.wallCatchedTime > this.ActionStatus.WallCatchTime))
                        {
                            this.CurrentState.IsWallCatching = true;
                            this.moveHorizontalAxis = 0f;
                        }
                    }

                    if (this.Operation.Jump.IsPressed)
                    {
                        if (this.CurrentState.IsLanding || this.CurrentState.IsWallCatching)
                        {
                            this.jumpPressedFlag = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ジャンプする
        /// 上方向に自身のジャンプ速度を足す
        /// </summary>
        protected void Jump()
        {
            this.VerticalSpeed += this.ActionStatus.JumpSpeed;
            this.ActionLock(this.ActionStatus.WallJumpCoolTime / 3);
        }

        protected virtual void WallJump()
        {
            this.VerticalSpeed += (this.ActionStatus.WallJumpSpeed + this.ActionStatus.OnWallFallSpeed);
            this.HorizontalSpeed = -this.FacingDirectionHorizontal * this.ActionStatus.RunSpeed;
            //this.RunSpeed = -this.FacingDirectionHorizontal * this.ActionStatus.RunSpeed;

            this.FacingDirectionHorizontal *= -1;
            this.wallCatchedTime = 0;
            this.ActionLock(this.ActionStatus.WallJumpCoolTime);
        }


        /// <summary>
        /// 毎フレームの更新
        /// 受け付けた入力を物理エンジンに反映
        /// </summary>
        protected override void FixedUpdate()
        {
            if (!this.IsFrozen)
            {
                if (!this.IsActionLocked)
                {
                    //this.RunSpeed = this.moveHorizontalAxis * this.ActionStatus.RunSpeed;
                    this.HorizontalSpeed = this.moveHorizontalAxis * this.ActionStatus.RunSpeed;

                    if (this.CurrentState.IsJumping)
                    {
                        if (!this.Operation.Jump.IsPressing)
                        {
                            this.VerticalSpeed *= 0.8f;
                        }
                    }

                    if (this.CurrentState.IsWallCatching)
                    {
                        this.VerticalSpeed = -this.ActionStatus.OnWallFallSpeed;
                    }
                    this.Rigidbody.AddForce(new Vector2(0, -this.ActionStatus.AdditionalGravity));

                    if (this.jumpPressedFlag)
                    {
                        if (this.CurrentState.IsLanding)
                        {
                            this.Jump();
                        }
                        if (this.CurrentState.IsWallCatching)
                        {
                            this.WallJump();
                        }
                        this.jumpPressedFlag = false;
                    }
                }
            }
            base.FixedUpdate();
        }

        protected virtual void ActionLock(float time)
        {
            this.IsActionLocked = true;
            this.Invoke("UnlockOperation", time);
        }

        protected virtual void UnlockOperation()
        {
            this.IsActionLocked = false;
            this.Operation.UnLock();
        }
    }
}