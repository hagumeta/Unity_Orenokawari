using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour {
    public GameObject EmitObject;
    public int EmitAmount;
    public float EmitPower;

	void Start () {

        for (int i=0; i<this.EmitAmount; i++)
        {
            var obj = Instantiate(this.EmitObject, this.transform);
            Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();

            Vector2 dir = new Vector2(Random.RandomRange(-0.5f, 0.5f), Random.RandomRange(-0.5f, 0.5f));
            rigid.AddForce(dir * this.EmitPower);
            rigid.angularVelocity = Random.RandomRange(-10020f, 10020f);
        }
    }
}