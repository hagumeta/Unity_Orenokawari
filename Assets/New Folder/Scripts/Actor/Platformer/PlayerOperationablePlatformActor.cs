using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤークラス
/// OperatuinablePlatformActorを継承
/// Killerタグのオブジェクトと衝突した際に死亡する処理を追加実装している．
/// </summary>
public class PlayerOperationablePlatformActor : OperationablePlatformActor, IPlayer {

    protected override void Start()
    {
        base.Start();
    }

    //シリアライズ：死亡時に発生させるオブジェクト
    [SerializeField]
    private GameObject deathEffectObject;
    public GameObject DeathEffectObject{
        get => this.deathEffectObject;
        set => this.deathEffectObject = value;
    }

    //このオブジェクトが死んでいるかどうか判定
    public bool IsDeath { get; set; }


    protected override void Update()
    {
        base.Update();
        ///プレイヤーのずり落ちを防止
        if (!this.CurrentState.IsRunning && this.CurrentState.IsLanding)
        {
            this.Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            this.Rigidbody.freezeRotation = true;
        }
        else
        {
            this.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


    /// <summary>
    /// Killerタグと当たったら死
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerKiller")
        {
            if (!this.IsDeath)
            {
                this.Death();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerKiller")
        {
            if (!this.IsDeath)
            {
                this.Death();
            }
        }
    }

    /// <summary>
    /// プレイヤーの死亡
    /// </summary>
    public virtual void Death()
    {
        this.IsDeath = true;
        Instantiate(this.DeathEffectObject, this.gameObject.transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject, Time.deltaTime);
    }
}