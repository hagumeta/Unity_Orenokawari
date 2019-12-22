using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extends.GameEvent;

namespace Game.Stage.GameEvents
{
    [CreateAssetMenu(fileName = "PlayerDeathEvent", menuName = "Player Death Event", order = 51)]
    public class PlayerDeathEvent : GameEvent<IPlayerDeathEventListener>
    {

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
}