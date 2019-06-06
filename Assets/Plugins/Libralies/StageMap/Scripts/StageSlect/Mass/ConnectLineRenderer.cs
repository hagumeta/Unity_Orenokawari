using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ConnectLineRenderer : MonoBehaviour {

    public List<Transform> points;

    public static Material DefaultMaterial;
    public Material defaultMaterial;


    private LineRenderer line;

    private void Start()
    {

        if (this.defaultMaterial != null)
        {
            DefaultMaterial = this.defaultMaterial;
        }


        this.line = this.GetComponent<LineRenderer>();
    }

    void Update () {
        this.line.material = DefaultMaterial;
        


        int count = 0;
        foreach (Transform point in this.points)
        {
            if (point != null) {
                this.line.SetPosition(count, point.position);
            }
            else
            {
                this.line.SetPosition(count, Vector3.zero);
            }
            count++;

        }
    }
}
