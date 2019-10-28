using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game;
using Game.Stage.Manager;
using Game.Stage.Event;
using System.Linq;

namespace Game.Stage.Displayer
{
    //public class DeathCountDisplayer : DeathCountChangedEventListener
    public class DeathCountDisplayer : CountDisplayer, IPlayerDeathEventListener
    {
        public void OnEventRaised(DeathType deathType)
        {
            this.UpdateCountText(Time.deltaTime);
        }

        public void OnEventRaised()
        {
            this.UpdateCountText(Time.deltaTime);
        }

        public override void UpdateCountText()
        {
            this.Count = StageManager.MyDeathCounts.SumCount;
        }
    }
}