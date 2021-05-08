using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Stage.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class IceBlock : MonoBehaviour, ITriggerSensorReceiver
    {
        private List<OnBlockObject> onBlockObjects = new List<OnBlockObject>();
        [SerializeField] private TriggerSensor triggerSensor;
        [SerializeField, Range(0f, 1f)] protected float Slippery = 0.95f;

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
            var remList = new List<OnBlockObject>();
            foreach (var obj in this.onBlockObjects)
            {
                if (obj.rigidbody != null && !obj.rigidbody.isKinematic)
                {
                    var vel = Mathf.Lerp(obj.rigidbody.velocity.x, obj.velocity.x, this.Slippery);
                    var kansei = new Vector2((vel) - obj.rigidbody.velocity.x, 0);
                    obj.UpdateVelocity(new Vector2(vel, 0));
                    obj.rigidbody.position += kansei * Time.deltaTime;
                }
                else
                {
                    remList.Add(obj);
                }
            }
            foreach (var obj in remList)
            {
                this.onBlockObjects.Remove(obj);
            }
        }


        public void OnTriggerSensorEnter(TriggerSensor sensor, Collider2D collider)
        {
            var rigid = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rigid)
            {
                if (!this.onBlockObjects.Exists(obj => obj.rigidbody == rigid))
                {
                    onBlockObjects.Add(new OnBlockObject(rigid));
                }
            }
        }

        public void OnTriggerSensorExit(TriggerSensor sensor, Collider2D collider)
        {
            var rigid = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rigid)
            {
                var obj = this.onBlockObjects.Find(obj => obj.rigidbody == rigid);
                if (obj.rigidbody != null)
                {
                    onBlockObjects.Remove(obj);
                }
            }
        }

        public void OnTriggerSensorStay(TriggerSensor sensor, Collider2D collider)
        {
        }


        class OnBlockObject
        {
            public OnBlockObject(Rigidbody2D rigidbody)
            {
                this.rigidbody  = rigidbody;
                this.velocity   = rigidbody.velocity;
            }

            public void UpdateVelocity(Vector2 velocity)
            {
                this.velocity = velocity;
            }
            public Rigidbody2D rigidbody;
            public Vector2 velocity;
        }
    }
}