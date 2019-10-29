using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Manager;
using Game.Stage.GameEvents;


namespace Game.Stage.Objects.Items
{
    public class MasterCoin : Coin
    {
        protected override void GetCoin()
        {
            this.coinGetEvent.Raise();
        }
    }
}