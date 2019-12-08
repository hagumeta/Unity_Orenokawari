using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Extends.CameraControlls
{
    [RequireComponent(typeof(Camera_TrackTransform))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private bool moveLock;

        public Transform TargetObject { get; private set; }
        public float FollowSpeed { get; private set; }
        private float defaultSize;
        private Vector3 defaultPosition;
        private new Camera camera;
        private Vector2 diff = Vector2.zero;
        private Abs_CameraContent[] cameraContents;

        public bool MoveLock
        {
            get => this.moveLock;
            set => this.moveLock = value;
        }

        public float FocusSize
        {
            get { return this.camera.orthographicSize; }
            set
            {
                DOTween.To(
                    () => this.camera.orthographicSize,
                size => this.camera.orthographicSize = size,
                value, 0.9f
                    ).SetEase(Ease.InExpo);
            }
        }

        void Start()
        {
            this.FollowSpeed = 0.05f;
            this.defaultPosition = this.transform.position;
            this.camera = this.GetComponent<Camera>();
            this.defaultSize = this.camera.orthographicSize;

            this.cameraContents = this.GetComponentsInChildren<Abs_CameraContent>();
        }

        void Update()
        {
            if (this.TargetObject != null && !this.moveLock)
            {
                var targetPos = new Vector3(this.TargetObject.transform.position.x, this.TargetObject.transform.position.y, this.defaultPosition.z) + (Vector3)this.diff;
                var distance = Vector2.Distance(this.transform.position, targetPos);
                if (distance >= this.FollowSpeed)
                {
                    var vec = (Vector3)Vector2.Lerp(this.transform.position, targetPos, this.FollowSpeed/distance);
                    vec.z = this.defaultPosition.z;
                    this.transform.position = vec;
                }
                else
                {
                    this.transform.position = targetPos;
                }
            }
        }



        public void FocusOnObject(Transform targetObjectTransform, float FocusSize, Vector2 diff)
        {
            this.TargetObject = targetObjectTransform;
            this.FocusSize = FocusSize;
            this.diff = diff;
        }
        public void FocusOnObject(Transform targetObjectTransform)
        {
            this.TargetObject = targetObjectTransform;
        }


        public void FocusPositionSetImmidiately()
        {
            if (this.TargetObject != null && !this.moveLock)
            {
                var targetPos = new Vector3(this.TargetObject.transform.position.x, this.TargetObject.transform.position.y, this.defaultPosition.z) + (Vector3)this.diff;
                this.transform.position = targetPos;
            }
        }
            public void UnFocus()
        {
            this.TargetObject = null;
        }

        public void ResetPosition()
        {
            this.transform.position = defaultPosition;
        }
        public void ResetSize()
        {
            this.FocusSize = defaultSize;
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
            if (timeForUnlock > 0)
            {
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
}