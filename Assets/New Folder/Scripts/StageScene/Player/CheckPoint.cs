using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Stage
{
    public class CheckPoint : MonoBehaviour, IPlayerTouchGimick
    {
        public bool IsLatest
        {
            get => this.latest;
        }
        protected bool latest;
        public Player playerType { protected set; get; }

        virtual public void OnPlayerTouched(Transform player)
        {
            var iPlayer = player.GetComponent<IPlayer>();
            this.playerType = GameManager.PlayerCollection.GetPlayer(iPlayer.playerType);

            var checkPointList = FindObjectsOfType<CheckPoint>();
            foreach (var point in checkPointList)
            {
                point.latest = false;
            }
            this.latest = true;
        }
    }
}