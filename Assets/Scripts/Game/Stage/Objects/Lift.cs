using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Lift : MonoBehaviour
    {
        private Vector2 prevPos;
        private Rigidbody2D rigidbody;
        private Vector2 liftVelocity;

        private List<Rigidbody2D> listOnLift = new List<Rigidbody2D>();

        void Start()
        {
            this.rigidbody = this.GetComponent<Rigidbody2D>();
            this.prevPos = this.rigidbody.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            this.liftVelocity = (this.rigidbody.position - this.prevPos);
            this.updateOnLiftVelocities();
            this.prevPos = this.rigidbody.position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var rigid = collision.transform.GetComponent<Rigidbody2D>();
            if (rigid != null && !rigid.isKinematic && !this.listOnLift.Exists(ri => ri.name == rigid.name))
            {
                this.listOnLift.Add(rigid);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            var rigid = collision.transform.GetComponent<Rigidbody2D>();
            if (rigid != null && !rigid.isKinematic && this.listOnLift.Exists(ri => ri.name == rigid.name))
            {
                this.listOnLift.Remove(rigid);
            }
        }


        private void updateOnLiftVelocities()
        {
            foreach (var rigid in this.listOnLift)
            {
                //rigid.velocity += this.liftVelocity * Time.deltaTime;
                rigid.position += this.liftVelocity;
            }
        }
    }
}