using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidbody.velocity = new Vector2(0, -0.1f);
        this.transform.position += new Vector3(0.1f, 0, 0);
    }
}
