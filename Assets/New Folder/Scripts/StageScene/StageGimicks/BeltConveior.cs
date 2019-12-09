using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Game.Stage.Objects
{
    public class BeltConveior : MonoBehaviour, ITriggerCensorReceiver
    {
        private List<Rigidbody2D> onBelt = new List<Rigidbody2D>();
        [SerializeField] private TriggerCensor triggerCensor;
        [SerializeField] protected float BeltSpeed;

        protected float FixedBeltSpeed
            => this.BeltSpeed * Time.fixedDeltaTime;

        private void Start()
        {
            this.SetCensor(this.triggerCensor);
        }

        public void SetCensor(TriggerCensor censor)
        {
            censor.SetTriggerCensor(this);
        }

        private void FixedUpdate()
        {
            var remList = new List<Rigidbody2D>();
            foreach (var rigid in this.onBelt)
            {
                if (rigid != null && !rigid.isKinematic)
                {
                    var pos = rigid.position;
                    pos.x += this.FixedBeltSpeed;
                    rigid.position = pos;
                }
                else
                {
                    remList.Add(rigid);
                }
            }
            this.onBelt.Except(remList);
        }


        public void OnTriggerCensorEnter(TriggerCensor censor, Collider2D collider)
        {
            var rigid = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rigid)
            {
                if (!onBelt.Contains(rigid))
                {
                    onBelt.Add(rigid);
                }
            }
        }

        public void OnTriggerCensorExit(TriggerCensor censor, Collider2D collider)
        {
            var rigid = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rigid)
            {
                if (onBelt.Contains(rigid))
                {
                    onBelt.Remove(rigid);
                }
            }
        }

        public void OnTriggerCensorStay(TriggerCensor censor, Collider2D collider)
        {
        }
    }
}