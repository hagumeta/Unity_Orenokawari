using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class OperationablePlatformActor : PlatformActor {
    
    //メンバ変数<インスペクタ表示>
    public ButtonOperation Operation;
    public ActionOfMovementStatus ActionStatus;
    public LayerMask layerMask;

    //入力を受けた結果，移動する方向(左：-1  右:1  動かない:0)
    private float moveHorizontalAxis;

    private float WallJumpCoolTime = 0.2f;
    private bool IsActionLocked = false;
    private float wallCatchedTime = 0;


    private bool jumpPressedFlag = false;

    /// <summary>
    /// 毎フレームの更新
    /// 入力を受け付ける
    /// </summary>
    protected override void Update ()
    {
        if (!this.IsFrozen) {
            base.Update();
            this.CurrentState.IsWallCatching = false;

            //this.moveHorizontalAxisの更新(ActionLockが掛かっているなら更新されない)
            if (!this.IsActionLocked)
            {
                //横方向の入力から進むべき方向を取得する
                if (this.Operation.Horizontal.IsPressing)
                {
                    //入力アリなら入力方向に設定(1 or -1 or 0)
                    RaycastHit2D hit, hit2, hit3;
                    hit = Physics2D.Linecast(this.transform.position + new Vector3(0, -0.12f), this.transform.position + new Vector3(this.Operation.Horizontal.AxisRaw * 0.26f, -0.3f), this.layerMask);
                    hit2 = Physics2D.Linecast(this.transform.position + new Vector3(0, -0f), this.transform.position + new Vector3(this.Operation.Horizontal.AxisRaw * 0.26f, -0.3f), this.layerMask);
                    hit3 = Physics2D.Linecast(this.transform.position + new Vector3(0, 0.1f), this.transform.position + new Vector3(this.Operation.Horizontal.AxisRaw * 0.26f, -0.5f), this.layerMask);

                    if (hit.transform == null && hit2.transform == null && hit3.transform == null)
                    {
                        this.moveHorizontalAxis = this.Operation.Horizontal.AxisRaw;
                    }
                    else
                    {
                        if (this.ActionStatus.CanWallJump)
                        {
                            if (this.CurrentState.IsFalling && !this.CurrentState.IsLanding)
                            {
                                if (hit2.transform != null || hit.transform != null || hit3.transform != null)
                                {
                                    this.CurrentState.IsWallCatching = true;
                                    this.FacingDirectionHorizontal = (int)this.Operation.Horizontal.AxisRaw;
                                    this.wallCatchedTime = Time.time;
                                }
                            }
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
        this.ActionLock(this.WallJumpCoolTime/3);
    }

    protected virtual void WallJump()
    {
        this.VerticalSpeed += (this.ActionStatus.WallJumpSpeed + this.ActionStatus.OnWallFallSpeed);
        this.HorizontalSpeed = -this.FacingDirectionHorizontal * this.ActionStatus.RunSpeed;
        this.FacingDirectionHorizontal *= -1;
        this.wallCatchedTime = 0;
        this.ActionLock(this.WallJumpCoolTime);
    }


    /// <summary>
    /// 毎フレームの更新
    /// 受け付けた入力を物理エンジンに反映
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!this.IsFrozen) {
            if (!this.IsActionLocked)
            {
                this.HorizontalSpeed = this.moveHorizontalAxis * this.ActionStatus.RunSpeed;

                if (this.CurrentState.IsJumping)
                {
                    if (!this.Operation.Jump.IsPressing) {
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

    //===========================================================================================================


    [System.Serializable]
    public class ActionOfMovementStatus
    {
        public float JumpSpeed, RunSpeed;
        public float OnWallFallSpeed, WallJumpSpeed, WallCatchTime, AdditionalGravity;
        public bool CanWallJump;
    }

    //===========================================================================================================

    [System.Serializable]
    public class ButtonOperation
    {
        public InputButton Horizontal, Vertical, Jump;
        public bool Locked
        {
            get => this._locked;
            private set
            {
                this.Horizontal.Locked = value;
                this.Vertical.Locked = value;
                this.Jump.Locked = value;
                this._locked = value;
            }
        }
        private bool _locked = false;

        [System.Serializable]
        public struct InputButton
        {
            public string ButtonName;
            internal bool Locked { get; set; }
            private bool _isPressed, _isPressing;
            private float _axisRaw, _axis;

            public bool IsPressed {
                get {
                    if (!this.Locked)
                    {
                        this._isPressed = Input.GetButtonDown(this.ButtonName);
                    }
                    else
                    {
                        this._isPressed = false;
                    }
                    return this._isPressed;
                }
            }
            public bool IsPressing
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._isPressing = Input.GetButton(this.ButtonName);
                    }
                    else
                    {
                        this._isPressing = false;
                    }
                    return this._isPressing;
                }
            }
            public float AxisRaw
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._axisRaw = Input.GetAxisRaw(this.ButtonName);
                    }
                    else
                    {
                        this._axisRaw = 0;
                    }
                    return this._axisRaw;
                }
            }
            public float Axis
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._axis = Input.GetAxis(this.ButtonName);
                    }
                    else
                    {
                        this._axis = 0;
                    }
                    return this._axis;
                }
            }
        }

        public void Lock()
        {
            this.Locked = true;
        }
        public void UnLock()
        {
            this.Locked = false;
        }
    }
}