using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Manager;
using Game.Stage.Event;


namespace Game.Stage.Gimicks
{
    public class MasterCoin : Coin
    {
        protected override void GetCoin()
        {
            this.coinGetEvent.Raise();
            //StageManager.MyCoinScore.GetMedal();
        }
    }
}