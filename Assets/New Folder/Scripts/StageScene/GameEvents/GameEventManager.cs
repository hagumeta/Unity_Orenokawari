using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class GameEventManager : SingletonMonoBehaviourFast<GameEventManager>
    {
        protected override void Init() { }

        [SerializeField] private GameEvent deathCountChangedEvent;
        [SerializeField] private GameEvent playerDeathEvent;
        [SerializeField] private GameEvent coinCountChangedEvent;

        public static GameEvent DeathCountChangedEvent
            => Instance.deathCountChangedEvent;
        public static GameEvent PlayerDeathEvent
            => Instance.playerDeathEvent;
        public static GameEvent CoinCountChangedEvent
            => Instance.coinCountChangedEvent;
    }
}
