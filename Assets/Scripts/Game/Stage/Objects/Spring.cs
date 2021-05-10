using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Game.Stage.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Spring : MonoBehaviour, ITriggerSensorReceiver
    {
        [SerializeField] private TriggerSensor triggerSensor;
        [SerializeField, Range(0f, 500f)] protected float Power = 10f;
        [SerializeField] protected float Delay = 0.05f;
        [SerializeField] protected UnityEvent OnJump;

        private bool inAction = false;

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
        }


        public void OnTriggerSensorEnter(TriggerSensor sensor, Collider2D collider)
        {
            if (this.inAction) return; 
            var rigid = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rigid && !rigid.isKinematic)
            {
                StartCoroutine(this.Jump(rigid));
            }
        }

        private IEnumerator Jump(Rigidbody2D rigid)
        {
            this.inAction = true;
            this.OnJump.Invoke();
            yield return new WaitForSeconds(this.Delay);
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(this.transform.up * this.Power, ForceMode2D.Impulse);
            yield return new WaitForSeconds(this.Delay);
            this.inAction = false;
        }


        public void OnTriggerSensorStay(TriggerSensor sensor, Collider2D collider)
        {
        }

        public void OnTriggerSensorExit(TriggerSensor sensor, Collider2D collider)
        {
        }
    }
}