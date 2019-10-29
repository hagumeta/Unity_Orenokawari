using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extends.GameEvent;

namespace Game.Stage.GameEvents
{
    [CreateAssetMenu(fileName = "StageClearEvent", menuName = "Stage Clear Event", order = 51)]
    public class StageClearEvent : GameEvent<IStageClearEventListener>
    {
        public void Raise(IPlayer player)
        {
            foreach (var listener in this.Listeners)
            {
                listener.OnEventRaised(player);
            }
        }
    }

    public interface IStageClearEventListener : IGameEventListener
    {
        void OnEventRaised(IPlayer player);        
    }
}