using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeInCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponent<Collider2D>().enabled = false;
        Invoke("Freeze", 0f);
    }

    private void Freeze()
    {
        var rigid = this.GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
