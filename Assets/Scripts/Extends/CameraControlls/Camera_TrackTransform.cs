using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Extends.CameraControlls
{
    /// <summary>
    /// Camera専用コンポーネント
    /// 設定したTransformを監視するように動く．
    /// 
    /// カメラ追尾モードは以下の二つ
    /// ・対象を一定の速度で追いかけるモード
    /// ・対象が画面外に行ったら対象の居る画面に移動するモード(参考：ロックマン/アイワナ 等)
    /// </summary>
    public class Camera_TrackTransform : Abs_CameraContent
    {
//        public Transform TrackingTargetObjectTransform;
        public string TrackingTargetObjectTag;
        public Vector2 TrackingCenterScreenPosition;
        public float TrackingSpeed;
        public bool IsTrackingSmooth;
        public bool IsFrozenScrollX, IsFrozenScrollY;


        protected bool isCameraLocked = false;

        private Vector3 targetPosition
        {
            get
            {
                Vector3 pos = (this.Camera.WorldToScreenPoint(this.TrackingTargetObjectTransform.position));
                return new Vector3(pos.x / Screen.width, pos.y / Screen.height);
            }
        }

        private Transform TrackingTargetObjectTransform
        {
            get
            {
                if (this.cacheTargetTransform == null)
                {
                    this.cacheTargetTransform = GameObject.FindGameObjectWithTag(this.TrackingTargetObjectTag).transform;
                }
                return this.cacheTargetTransform;
            }
        }
        private Transform cacheTargetTransform;

        private Vector3 camerapos;
        private Vector3 cameraPosition
        {
            set
            {
                value.z = this.transform.position.z;
                if (this.IsTrackingSmooth) { this.transform.position = value; }
                if (this.camerapos == value) { return; }
                if (this.IsFrozenScrollX) { value.x = this.cameraPosition.x; }
                if (this.IsFrozenScrollY) { value.y = this.cameraPosition.y; }
                this.camerapos = value;
                this.transform.DOKill();
                this.transform.DOMove(this.camerapos, 0.2f);
            }
            get => this.camerapos;
        }

        private bool IsTargetObjectOutOfCamera
        {
            get
            {
                if (this.TrackingTargetObjectTransform == null)
                    return false;
                else
                    return (this.targetPosition.x < 0 || this.targetPosition.x > 1
                                || this.targetPosition.y > 1 || this.targetPosition.y < 0);
            }
        }


        private void Start()
        {
            this.camerapos = this.transform.position;
        }

        private void Update()
        {
            if (!this.isCameraLocked && this.TrackingTargetObjectTransform != null)
            {
                if (this.IsTrackingSmooth)
                {
                    this.TrackObjectSmooth();
                }
                else
                {
                    this.TrackObjectDiveded();
                }
            }
        }


        private void TrackObjectSmooth()
        {
            Vector2 pos = (this.TrackingCenterScreenPosition) * this.CameraSizeInWorld;
            Vector3 target = this.TrackingTargetObjectTransform.position - (Vector3)pos;
            Vector3 move = Vector3.zero;
            if (!this.IsFrozenScrollX)
            {
                move.x = target.x - this.cameraPosition.x;
            }
            if (!this.IsFrozenScrollY)
            {
                move.y = target.y - this.cameraPosition.y;
            }
            if (move.sqrMagnitude > 0 && move.sqrMagnitude < this.TrackingSpeed)
            {
                this.cameraPosition += move;
            } 
            else
            {
                this.cameraPosition += move.normalized * this.TrackingSpeed;
            }

        }

        private void TrackObjectDiveded()
        {
            if (this.IsTargetObjectOutOfCamera)
            {
                this.isCameraLocked = true;
                this.Invoke("UnlockCamera", 0.2f);
                if (this.targetPosition.x > 1) this.cameraPosition += new Vector3(this.CameraSizeInWorld.x, 0);
                if (this.targetPosition.x < 0) this.cameraPosition -= new Vector3(this.CameraSizeInWorld.x, 0);
                if (this.targetPosition.y > 1) this.cameraPosition += new Vector3(0, this.CameraSizeInWorld.y);
                if (this.targetPosition.y < 0) this.cameraPosition -= new Vector3(0, this.CameraSizeInWorld.y);
            }
        }

        private void UnlockCamera()
        {
            this.isCameraLocked = false;
        }



        private void OnDrawGizmos()
        {
            if (!this.IsTrackingSmooth) {
                float cnt_x = 20;
                float cnt_y = 4;

                Gizmos.color = Color.cyan;
                for (int x = 0; x < cnt_x; x++)
                {
                    for (int y = 0; y < cnt_y; y++)
                    {
                        Vector2 pos = this.transform.position + new Vector3((x - cnt_x / 2) * this.CameraSizeInWorld.x
                                                                        , (y - cnt_y / 2) * this.CameraSizeInWorld.y);
                        Gizmos.DrawWireCube(pos, this.CameraSizeInWorld);
                    }
                }
            }
        }

    }
}