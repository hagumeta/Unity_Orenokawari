using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CameraControll;

[RequireComponent(typeof(Camera_TrackTransform))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private bool move_lock;

    private Transform FocusObject;
    private float defaultSize;
    private Vector3 defaultPosition;
    private new Camera camera;
    private Vector2 diff = Vector2.zero;

    private Abs_CameraContent[] cameraContents;

    public bool Move_lock
    {
        get => this.move_lock;
        set => this.move_lock = value;
    }

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


        this.cameraContents = this.GetComponentsInChildren<Abs_CameraContent>();
    }

    void Update()
    {
        if (this.FocusObject != null && !this.move_lock)
        {
            this.transform.position = new Vector3(this.FocusObject.transform.position.x, this.FocusObject.transform.position.y, this.defaultPosition.z) + (Vector3)this.diff;
        }
    }

    

    public void FocusOnObject(Transform targetObjectTransform, float FocusSize, Vector2 diff)
    {
        this.FocusObject = targetObjectTransform;
        this.FocusSize = FocusSize;
        this.diff = diff;
    }
    public void FocusOnObject(Transform targetObjectTransform)
    {
        this.FocusObject = targetObjectTransform;
    }

    public void UnFocus() {
        this.FocusObject = null;
        this.FocusSize = defaultSize;
        this.transform.position = defaultPosition;
    }



    private List<Abs_CameraContent> unLockList;
    public void Lock(float timeForUnlock = -1f)
    {
        this.unLockList = new List<Abs_CameraContent>();
        foreach (var content in this.cameraContents)
        {
            if (content.isActiveAndEnabled)
            {
                this.unLockList.Add(content);
                content.enabled = false;
            }
        }
        if (timeForUnlock > 0) {
            this.Invoke("UnLock", timeForUnlock);
        }
    }
    public void UnLock()
    {
        if (this.unLockList == null) { return; }
        foreach (var content in this.unLockList)
        {
            content.enabled = true;
        }
        this.unLockList = null;
    }
}