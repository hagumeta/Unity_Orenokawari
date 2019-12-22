using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyListener : MonoBehaviour
{
    public UnityEvent Event;
    public string Button;
    void Update()
    {
        if (Input.GetButtonDown(this.Button))
        {
            this.Event.Invoke();
        }
    }
}