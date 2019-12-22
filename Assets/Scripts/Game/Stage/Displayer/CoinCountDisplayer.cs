using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Stage.Manager;
using Game.Stage.GameEvents;
using Game.Data;

namespace Game.Stage.Displayer
{
    //    public class CoinCountDisplayer : CoinCountChangedEventListener
    public class CoinCountDisplayer : CountDisplayer, ICoinGetEventListener
    {
        public override void UpdateCountText()
        {
            this.text.text = StageManager.MyCoinScore.CoinStatus();
        }

        public void OnEventRaised(int coinNum)
        {
            this.UpdateCountText(Time.deltaTime);
        }

        public void OnEventRaised()
        {
            this.UpdateCountText(Time.deltaTime);
        }

    }
}