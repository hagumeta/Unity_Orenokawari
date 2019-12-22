using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.CameraControlls
{
    public class CameraFocusThis : MonoBehaviour
    {
        [SerializeField] private bool cameraLock;
        private CameraController _cameraController;
        public CameraController cameraController
        {
            get
            {
                if (this._cameraController == null)
                {
                    this.setCameraController();
                }
                return this._cameraController;
            }
        }

        private void setCameraController()
        {
            if (Camera.main != null) {
                this._cameraController = Camera.main.GetComponent<CameraController>();
                if (this.cameraController == null)
                {
                    Debug.Log("Main cameraにcameraControllerつけ忘れてるよ");
                    this.enabled = false;
                }
            }
        }

        private void OnEnable()
        {
            this.cameraController.FocusOnObject(this.transform);
            this.cameraController.MoveLock = this.cameraLock;
        }

        private void OnDestroy()
        {
            this.UnFocusThis();
        }

        private void OnDisable()
        {
            this.UnFocusThis();
        }

        private void UnFocusThis()
        {
            if(this.cameraController != null 
                && this.cameraController.TargetObject == this.transform)
            {
                this.cameraController.UnFocus();
            }
        }
    }
}