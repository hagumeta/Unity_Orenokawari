using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerSensor : MonoBehaviour
{
    protected ITriggerSensorReceiver receiver;
    public void SetTriggerSensor(ITriggerSensorReceiver receiver)
    {
        this.receiver = receiver;
    }
    protected bool isTriggerEnter;
    protected bool isTriggerStay;
    protected bool isTriggerExit;

    public bool IsTriggerEnter
        => this.isTriggerEnter;
    public bool IsTriggerStay
        => this.IsTriggerStay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger) {
            receiver.OnTriggerSensorEnter(this, collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            receiver.OnTriggerSensorStay(this, collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            receiver.OnTriggerSensorExit(this, collision);
        }
    }
}


public interface ITriggerSensorReceiver
{
    void OnTriggerSensorEnter(TriggerSensor sensor, Collider2D collider);
    void OnTriggerSensorExit(TriggerSensor sensor, Collider2D collider);
    void OnTriggerSensorStay(TriggerSensor sensor, Collider2D collider);

    void SetSensor(TriggerSensor sensor);
}