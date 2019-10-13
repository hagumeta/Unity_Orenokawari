using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputUtil
{
    public class ButtonOperation
    {
        protected InputButton _horizontal, _vertical, _submit, _cancel;
        private bool _locked = false;
        private static ButtonOperation instance;

        public static bool Locked
        {
            get => instance._locked;
            private set
            {
                instance._horizontal.Locked = value;
                instance._vertical.Locked = value;
                instance._submit.Locked = value;
                instance._cancel.Locked = value;
                instance._locked = value;
            }
        }
        public static InputButton Horizontal
        {
            get => instance._horizontal;
        }
        public static InputButton Vertical
        {
            get => instance._vertical;
        }
        public static InputButton Submit
        {
            get => instance._submit;
        }
        public static InputButton Cancel
        {
            get => instance._cancel;
        }


        public ButtonOperation()
        {
            instance = this;
            this._horizontal = new InputButton("Horizontal");
            this._vertical = new InputButton("Vertical");
            this._submit = new InputButton("Jump");
            this._cancel = new InputButton("Cancel");
        }


        [System.Serializable]
        public class InputButton
        {
            public InputButton() { }
            public InputButton(string buttonName)
            {
                this.ButtonName = buttonName;
            }

            public string ButtonName;
            public bool Locked { get; set; }
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
    }
}