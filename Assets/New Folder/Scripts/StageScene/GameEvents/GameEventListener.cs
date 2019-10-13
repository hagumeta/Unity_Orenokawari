﻿using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    protected GameEvent gameEvent;
/*    [SerializeField]
    private UnityEvent response;
    */
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
    virtual public void OnEventRaised()
    {
    }
}