using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.GameEvents;
using Game.Data.Dynamic;

namespace Game.Stage.Manager
{
    public class CoinScoreManager : MonoBehaviour, ICoinGetEventListener, IManager
    {
        public CoinScore CoinScore { get; private set; }

        public void OnEventRaised(int coinNum)
        {
            this.CoinScore.GetCoin();
        }

        public void OnEventRaised()
        {
            this.CoinScore.GetMedal();
        }

        public void Init(int stageID)
        {
            this.CoinScore = new CoinScore(stageID);
        }
    }
}