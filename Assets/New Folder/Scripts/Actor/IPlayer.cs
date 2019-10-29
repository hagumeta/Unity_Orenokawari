using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer{

    /// <summary>
    /// プレイヤーの死亡
    /// </summary>
    void Death();

    /// <summary>
    /// 死亡時に生成するエフェクトオブジェクト
    /// </summary>
    GameObject DeathEffectObject { get; set; }

    /// <summary>
    /// 自身は死んでいるかどうか
    /// </summary>
    bool IsDeath { get; set; }
}