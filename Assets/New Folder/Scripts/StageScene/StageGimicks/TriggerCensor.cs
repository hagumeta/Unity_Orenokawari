using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerCensor : MonoBehaviour
{
    protected ITriggerCensorReceiver receiver;
    public void SetTriggerCensor(ITriggerCensorReceiver receiver)
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
        receiver.OnTriggerCensorEnter(this, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        receiver.OnTriggerCensorStay(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        receiver.OnTriggerCensorExit(this, collision);
    }
}


public interface ITriggerCensorReceiver
{
    void OnTriggerCensorEnter(TriggerCensor censor, Collider2D collider);
    void OnTriggerCensorExit(TriggerCensor censor, Collider2D collider);
    void OnTriggerCensorStay(TriggerCensor censor, Collider2D collider);

    void SetCensor(TriggerCensor censor);
}