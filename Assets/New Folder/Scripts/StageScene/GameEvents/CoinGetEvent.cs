using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Manager;
using Extends.GameEvent;

namespace Game.Stage.GameEvents
{
    [CreateAssetMenu(fileName = "CoinGetEvent", menuName = "Coin Get Event", order = 53)]
    public class CoinGetEvent : GameEvent<ICoinGetEventListener>
    {
        public void Raise(int num)
        {
            foreach (var listener in base.Listeners)
            {
                listener.OnEventRaised(num);
            }
        }
    }

    public interface ICoinGetEventListener : IGameEventListener
    {
        void OnEventRaised(int coinNum);
    }
}