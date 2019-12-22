using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Stage.Objects
{
    public class StageGimick_ButtonSwitch : MonoBehaviour
    {
        public GameObject PressedDesignObject;
        public GameObject UnPressedDesignObject;
        public UnityEvent PressedEvent;
        public UnityEvent ReleasedEvent;

        public bool IsPressed { get; set; }
        private Collider2D _collider;
        protected Collider2D collider
        {
            get
            {
                if (this._collider == null)
                {
                    this._collider = this.GetComponentInChildren<Collider2D>();
                }
                return this._collider;
            }
        }
        private List<Transform> pushingObjectList;


        void Start()
        {
            this.collider.isTrigger = true;
            this.pushingObjectList = new List<Transform>();
            this.ReleaseAction();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            var isFirstPressed = this.pushingObjectList.Count == 0;
            this.pushingObjectList.Add(collision.transform);
            if (isFirstPressed)
            {
                this.PressedAction();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            this.pushingObjectList.Remove(collision.transform);
            if (this.pushingObjectList.Count == 0)
            {
                this.ReleaseAction();
            }
        }


        protected void PressedAction()
        {
            this.PressedDesignObject.SetActive(true);
            this.UnPressedDesignObject.SetActive(false);
            this.IsPressed = true;

            this.PressedEvent.Invoke();
        }

        protected void ReleaseAction()
        {
            this.PressedDesignObject.SetActive(false);
            this.UnPressedDesignObject.SetActive(true);
            this.IsPressed = false;

            this.ReleasedEvent.Invoke();
        }
    }
}