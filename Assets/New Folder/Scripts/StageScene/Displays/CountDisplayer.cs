using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace Game.Stage.Displayer
{
    public interface ICountDisplayer
    {
        TextMeshProUGUI Text { get; }
        int Count { get; set; }

        void UpdateCountText();
    }

    public abstract class CountDisplayer : MonoBehaviour, ICountDisplayer
    {
        [SerializeField] protected TextMeshProUGUI text;
        [SerializeField] protected int count;

        public TextMeshProUGUI Text => this.text;

        public virtual int Count
        {
            get => this.count;
            set
            {
                if (this.count != value && this.text != null)
                {
                    this.text.text = value.ToString();
                }
            }
        }

        public virtual void UpdateCountText() { }

        public void UpdateCountText(float dilay = 0.01f)
        {
            if (dilay <= 0)
            {
                this.UpdateCountText();
            } else {
                this.Invoke("UpdateCountText", dilay);
            }
        }

        private void Start()
        {
            this.UpdateCountText();
        }
    }
}