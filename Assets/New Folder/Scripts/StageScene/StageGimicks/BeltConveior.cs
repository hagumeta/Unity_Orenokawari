using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Game.Stage.Objects
{
    public class BeltConveior : MonoBehaviour, ITriggerSensorReceiver
    {
        private List<Rigidbody2D> onBelt = new List<Rigidbody2D>();
        [SerializeField] private TriggerSensor triggerSensor;
        [SerializeField] protected Vector2 BeltSpeed;

        protected Vector2 FixedBeltSpeed
            => this.BeltSpeed * Time.fixedDeltaTime;

        private void Start()
        {
            this.SetSensor(this.triggerSensor);
        }

        public void SetSensor(TriggerSensor sensor)
        {
            sensor.SetTriggerSensor(this);
        }

        private void FixedUpdate()
        {
            var remList = new List<Rigidbody2D>();
            foreach (var rigid in this.onBelt)
            {
                if (rigid != null && !rigid.isKinematic)
                {
                    rigid.position += this.FixedBeltSpeed;
                }
                else
                {
                    remList.Add(rigid);
                }
            }
            this.onBelt.Except(remList);
        }


        public void OnTriggerSensorEnter(TriggerSensor sensor, Collider2D collider)
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

        public void OnTriggerSensorExit(TriggerSensor sensor, Collider2D collider)
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

        public void OnTriggerSensorStay(TriggerSensor sensor, Collider2D collider)
        {
        }
    }
}