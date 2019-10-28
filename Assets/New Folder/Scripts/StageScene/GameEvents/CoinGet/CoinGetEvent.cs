using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Manager;

namespace Game.Stage.Event
{

    [CreateAssetMenu(fileName = "New Game Event", menuName = "Coin Get Event", order = 53)]
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

    /*
    public abstract class CoinGetEventListener : GameEventListener
    {
        public void OnEventRaised(int num)
        {
            this.OnCoinGetting(num);
        }
        public override void OnEventRaised()
        {
            this.OnMedalGetting();
        }

        protected abstract void OnCoinGetting(int num);
        protected abstract void OnMedalGetting();
    }
    */
}