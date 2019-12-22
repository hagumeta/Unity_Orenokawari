using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpinner : MonoBehaviour {

    public float SpinSpeed;
    public Axis axis = Axis.z;

    public enum Axis
    {
        x, y, z
    }


	void FixedUpdate () {

        Vector3 sp;
        switch (this.axis)
        {
            case Axis.x:
                sp = new Vector3(this.SpinSpeed, 0f, 0f);
                break;
            case Axis.y:
                sp = new Vector3(0f, this.SpinSpeed, 0f);
                break;
            case Axis.z:
                sp = new Vector3(0f, 0f, this.SpinSpeed);
                break;
            default:
                sp = Vector3.zero;
                break;
        }
        this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.eulerAngles + sp);
    }
}