using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.Sounds;

namespace Menu
{
    public class MenuCursor : MonoBehaviour
    {
        [System.Serializable]
        public class MenuIndex
        {
            public Transform indexTransform;
            public UnityEvent SubmittedEvent;
            public UnityEvent SelectedEvent;
        }
        [SerializeField]
        public MenuIndex[] MenuIndexes;
        public int DefaultCursorIndex = 0;


        private int _cursor = -1;
        public int cursor
        {
            get
            {
                return this._cursor;
            }
            private set
            {
                int index = Mathf.Clamp(value, 0, this.MenuIndexes.Length - 1);
                if (index != this._cursor)
                {
                    this._cursor = index;
                    this.transform.position = this.MenuIndexes[this.cursor].indexTransform.position;
                    this.MenuIndexes[this.cursor].SelectedEvent.Invoke();
                    GlobalAudioPlayer.PlayAudio(GlobalAudioCollection.SESetting.MenuSelectSound);
                }
            }
        }

        void Start()
        {
            this.cursor = this.DefaultCursorIndex;
        }

        private void OnEnable()
        {
            this.cursor = this.DefaultCursorIndex;
        }


        void Update()
        {
            if (Input.GetButtonDown("Vertical"))
            {
                int verticalAxis = (int)Input.GetAxisRaw("Vertical");
                if (verticalAxis != 0)
                {
                    this.cursor -= verticalAxis;
                }
            }

            if (Input.GetButtonDown("Submit"))
            {
                GlobalAudioPlayer.PlayAudio(GlobalAudioCollection.SESetting.MenuEnterSound);
                this.MenuIndexes[this.cursor].SubmittedEvent.Invoke();
            }
        }

        virtual protected void Enter(){        }
    }
}