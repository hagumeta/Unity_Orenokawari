using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Extends.Cursor
{
    public abstract class Cursor : MonoBehaviour
    {

        public float moveSpeed;
        public bool isActivate = true;
        public Mass currentMass;
        public bool IsMoving
        {
            get
            {
                return DOTween.IsTweening(this.transform);
            }
        }

        void Start()
        {
            this.MoveToMass(this.currentMass);
        }

        void Update()
        {
            if (this.isActivate)
            {
                var hInput = Input.GetAxisRaw("Horizontal");
                var vInput = Input.GetAxisRaw("Vertical");

                if (hInput == -1)
                {
                    this.MoveToMass(this.currentMass.next.left);
                    return;
                }
                if (hInput == 1)
                {
                    this.MoveToMass(this.currentMass.next.right);
                    return;
                }
                if (vInput == -1)
                {
                    this.MoveToMass(this.currentMass.next.down);
                    return;
                }
                if (vInput == 1)
                {
                    this.MoveToMass(this.currentMass.next.up);
                    return;
                }

                this.WaitInput();
            }
        }


        public void MoveToMass(Mass nextMass, bool immediate = false)
        {
            if (nextMass != null && nextMass.IsActive)
            {
                if (immediate)
                {
                    this.transform.DOKill();
                    this.MoveTo(nextMass.transform, 10000f);
                }
                else
                {
                    this.MoveTo(nextMass.transform, this.moveSpeed);
                }
                this.currentMass = nextMass;
            }
        }

        private void MoveTo(Transform target, float moveSpeed)
        {
            this.BeforeMoveAction();

            var dist = (target.position - this.transform.position).magnitude;
            if (moveSpeed > 0)
            {
                float time = Mathf.Clamp(dist / moveSpeed, Time.deltaTime, 2f);

                this.transform.DOMove(target.transform.position, time).SetEase(Ease.Linear);
                this.Lock(time - 0.05f);
            }
        }

        protected void Lock(float unlockTime = 10f)
        {
            this.isActivate = false;
            if (unlockTime > 0)
            {
                Invoke("UnLock", unlockTime);
            }
            else
            {
                Invoke("UnLock", Time.deltaTime);
            }
        }
        protected void UnLock()
        {
            this.EndMoveAction();
            this.isActivate = true;
        }


        abstract protected void BeforeMoveAction();
        abstract protected void EndMoveAction();
        abstract protected void WaitInput();
    }
}