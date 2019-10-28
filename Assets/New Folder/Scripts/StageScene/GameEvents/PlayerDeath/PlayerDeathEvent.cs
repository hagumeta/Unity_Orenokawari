using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


namespace Game.Stage.Event
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Player Death Event", order = 51)]
    public class PlayerDeathEvent : GameEvent<IPlayerDeathEventListener>
    {
        /*
        public void Raise(DeathType deathType)
        {
            for (int i = Listeners.Count - 1; i >= 0; i--)
            {
                Listeners[i].OnEventRaised(deathType);
            }
        }*/
        public void Raise(DeathType deathType)
        {
            foreach (var listener in this.Listeners)
            {
                listener.OnEventRaised(deathType);
            }
        }
        public override void Raise()
        {
            this.Raise(DeathType.StickingDeath);
        }
    }

    public interface IPlayerDeathEventListener : IGameEventListener
    {
        void OnEventRaised(DeathType deathType);
    }
/*
    public abstract class PlayerDeathEventListener : GameEventListener
    {
        protected new GameEvent<PlayerDeathEventListener> gameEvent
        {
            get => GameEventManager.PlayerDeathEvent;
        }

        public override void OnEventRaised()
        {
            this.OnPlayerDeath();
        }
        public void OnEventRaised(DeathType deathType)
        {
            this.OnPlayerDeath(deathType);
        }

        public abstract void OnPlayerDeath(DeathType deathType = DeathType.StickingDeath);
    }
    */
}