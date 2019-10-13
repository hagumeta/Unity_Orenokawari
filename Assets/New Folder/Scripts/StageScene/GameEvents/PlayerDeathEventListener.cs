using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public abstract class PlayerDeathEventListener : GameEventListener
{
    protected new GameEvent gameEvent
    {
        get => GameEventManager.PlayerDeathEvent;
    }

    public override void OnEventRaised()
    {
        this.OnPlayerDeath();
    }

    public abstract void OnPlayerDeath();
}