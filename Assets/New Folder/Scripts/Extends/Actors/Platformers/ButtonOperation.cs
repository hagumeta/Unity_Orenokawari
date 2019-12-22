using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Actors.Platformers
{
    [System.Serializable]
    public class ButtonOperation
    {
        public InputButton Horizontal, Vertical, Jump;
        public bool Locked
        {
            get => this._locked;
            private set
            {
                this.Horizontal.Locked = value;
                this.Vertical.Locked = value;
                this.Jump.Locked = value;
                this._locked = value;
            }
        }
        private bool _locked = false;

        [System.Serializable]
        public struct InputButton
        {
            public string ButtonName;
            internal bool Locked { get; set; }
            private bool _isPressed, _isPressing;
            private float _axisRaw, _axis;

            public bool IsPressed
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._isPressed = Input.GetButtonDown(this.ButtonName);
                    }
                    else
                    {
                        this._isPressed = false;
                    }
                    return this._isPressed;
                }
            }
            public bool IsPressing
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._isPressing = Input.GetButton(this.ButtonName);
                    }
                    else
                    {
                        this._isPressing = false;
                    }
                    return this._isPressing;
                }
            }
            public float AxisRaw
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._axisRaw = Input.GetAxisRaw(this.ButtonName);
                    }
                    else
                    {
                        this._axisRaw = 0;
                    }
                    return this._axisRaw;
                }
            }
            public float Axis
            {
                get
                {
                    if (!this.Locked)
                    {
                        this._axis = Input.GetAxis(this.ButtonName);
                    }
                    else
                    {
                        this._axis = 0;
                    }
                    return this._axis;
                }
            }
        }

        public void Lock()
        {
            this.Locked = true;
        }
        public void UnLock()
        {
            this.Locked = false;
        }
    }
}