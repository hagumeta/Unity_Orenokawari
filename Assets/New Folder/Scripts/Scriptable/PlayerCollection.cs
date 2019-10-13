using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game;

namespace Game.Stage
{
    [CreateAssetMenu(fileName = "PlayerCollection", menuName = "Option/PlayerCollection")]
    public class PlayerCollection : ScriptableObject
    {
        [SerializeField]
        private List<Player> playerList;
        public List<Player> PlayerList
        {
            get => this.playerList;
        }

        public Player GetPlayer(PlayerType playerType)
        {
            if (this.playerList == null)
            {
                return null;
            }
            return this.playerList.Single(a => a.playerType == playerType);
        }
    }

    [System.Serializable]
    public class Player
    {
        public PlayerType playerType;
        public GameObject PlayerObject;
        [SerializeField]
        public List<Corpse> corpseCollection;

        [System.Serializable]
        public class Corpse
        {
            public DeathType deathType;
            public GameObject CorpseObject;
        }

        public GameObject namusanObject;
        public AudioClip namusanSound;
    }

    public static class ExDeathCollection
    {
        public static GameObject GetCorpseObject(this List<Player.Corpse> corpse, DeathType deathType)
        {
            return corpse
                .Single(a => a.deathType == deathType)
                .CorpseObject;
        }
    }


    public enum PlayerType
    {
        Stand2DAction,
        RPGField,

    }
}