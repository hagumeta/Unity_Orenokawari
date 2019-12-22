using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.GameEvents;

namespace Game
{
    public class GameEventManager : SingletonMonoBehaviourFast<GameEventManager>
    {
        protected override void Init() { }

        [SerializeField] private PlayerDeathEvent playerDeathEvent;
        [SerializeField] private CoinGetEvent coinGetEvent;

        public static PlayerDeathEvent PlayerDeathEvent
            => Instance.playerDeathEvent;
        public static CoinGetEvent GetCoinEvent
            => Instance.coinGetEvent;
    }
}