using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Event;

namespace Game
{
    public class GameEventManager : SingletonMonoBehaviourFast<GameEventManager>
    {
        protected override void Init() { }

        //[SerializeField] private GameEvent deathCountChangedEvent;
        [SerializeField] private PlayerDeathEvent playerDeathEvent;
        [SerializeField] private CoinGetEvent coinGetEvent;

        //public static GameEvent DeathCountChangedEvent
        //    => Instance.deathCountChangedEvent;
        public static PlayerDeathEvent PlayerDeathEvent
            => Instance.playerDeathEvent;
        public static CoinGetEvent GetCoinEvent
            => Instance.coinGetEvent;
    }
}
