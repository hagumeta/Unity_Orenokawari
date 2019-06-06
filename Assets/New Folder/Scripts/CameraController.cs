using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public GameObject FocusObject;
    private float defaultSize;
    private Vector3 defaultPosition;
    private new Camera camera;
    private Vector2 diff = Vector2.zero;
    private float FocusSize
    {
        get { return this.camera.orthographicSize; }
        set
        {
            DOTween.To(
                () => this.camera.orthographicSize,
            size => this.camera.orthographicSize = size,
            value, 1f
                ).SetEase(Ease.InExpo);
        }
    }


    void Start()
    {
        this.defaultPosition = this.transform.position;
        this.camera = this.GetComponent<Camera>();
        this.defaultSize = this.camera.orthographicSize;
    }

    void Update()
    {
        if (this.FocusObject != null)
        {
            this.transform.position = new Vector3(this.FocusObject.transform.position.x, this.FocusObject.transform.position.y, this.defaultPosition.z) + (Vector3)this.diff;
        }
    }


    public void FocusOnObject(GameObject targetObject, float FocusSize, Vector2 diff)
    {
        this.FocusObject = targetObject;
        this.FocusSize = FocusSize;
        this.diff = diff;
    }


    public void UnFocus() {
        this.FocusObject = null;
        this.FocusSize = defaultSize;
        this.transform.position = defaultPosition;
    }
}