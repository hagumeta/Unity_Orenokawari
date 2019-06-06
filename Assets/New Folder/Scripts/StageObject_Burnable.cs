using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 可燃性なステージオブジェクトにつける．
/// 他のFireオブジェクトに当たると自身も燃え上がる
/// </summary>
public class StageObject_Burnable : MonoBehaviour
{
    public GameObject FireStageObject;
    protected bool isBurnning;
    public float toBurnTime = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.isBurnning && collision.gameObject.tag == "PlayerKiller_Fire")
        {
            this.isBurnning = true;
            Invoke("BurnUp", this.toBurnTime);
        } 
    }

    protected void BurnUp()
    {
        var fire = Instantiate(this.FireStageObject, this.transform);
        fire.transform.localPosition = Vector3.zero;
    }
}