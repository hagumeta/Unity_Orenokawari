using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEvents : MonoBehaviour
{
    public UnityEvent[] Events;

    public void Invoke(int index)
    {
        this.Events[index].Invoke();
    }

    public void InvokeAll()
    {
        foreach (var _event in this.Events) {
            _event.Invoke();
        }
    }
}
