using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.StageSelect
{
    public class StickMan_RunningIcon : MonoBehaviour
    {
        [SerializeField]
        protected Animator animator;

        private Vector3 prevPosition;
        private bool IsMoving;

        private float HorizontalScale
        {
            set
            {
                if (value == 0) { return; }
                float scale;
                if (value > 0)
                {
                    scale = this.hDesScale;
                }
                else
                {
                    scale = -this.hDesScale;
                }
                this.transform.localScale = new Vector3(scale, this.transform.localScale.y);
            }
        }
        private float hDesScale;

        private void Start()
        {
            this.hDesScale = this.transform.localScale.x;
            this.prevPosition = this.transform.position;
        }

        void FixedUpdate()
        {
            this.IsMoving = (this.transform.position - this.prevPosition).magnitude > 0.003f;
            if (this.animator.GetBool("IsMoving") != this.IsMoving) {
                this.animator.SetBool("IsMoving", this.IsMoving);
            }
            if (this.IsMoving)
            {
                this.HorizontalScale = this.transform.position.x - this.prevPosition.x;
            }

            this.prevPosition = this.transform.position;
        }
    }
}