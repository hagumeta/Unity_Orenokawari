using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extends.Motions
{
    public class AutoScroll : MonoBehaviour
    {
        [SerializeField] private Vector2 speedPerSeconds;
        [SerializeField] private Vector2 turnMinPos;
        [SerializeField] private Vector2 turnMaxPos;

        private Vector2 speedPerFrame
            => this.speedPerSeconds * Time.deltaTime;

        void Start()
        {

        }

        private void FixedUpdate()
        {
            Vector3 nextPos = this.transform.position + (Vector3)this.speedPerFrame;

            if (nextPos.y > this.turnMaxPos.y)
            {
                nextPos.y = this.turnMinPos.y;
            }
            if (nextPos.x > this.turnMaxPos.x)
            {
                nextPos.x = this.turnMinPos.x;
            }
            if (nextPos.y < this.turnMinPos.y)
            {
                nextPos.y = this.turnMaxPos.y;
            }
            if (nextPos.x < this.turnMinPos.x)
            {
                nextPos.x = this.turnMaxPos.x;
            }

            this.transform.position = nextPos;
        }
    }
}