using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpinner : MonoBehaviour {

    public float SpinSpeed;

	void Update () {
        this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.eulerAngles + new Vector3(0f, 0f, this.SpinSpeed));
	}
}