using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Stage.Gimicks
{
    public class MasterCoin : Coin
    {
        protected override void GetCoin()
        {
            StageManager.MyCoinScore.GetMedal();
        }
    }
}