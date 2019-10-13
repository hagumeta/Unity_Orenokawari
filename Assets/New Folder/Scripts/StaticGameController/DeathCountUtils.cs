using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Game
{
    public class DeathCounts
    {
        public Dictionary<DeathType, int> deathes { get; private set; }
        public int StageID { get; private set; }

        public void CountUp(DeathType deathType)
        {
            if (this.deathes.Keys.Contains(deathType))
            {
                this.deathes[deathType]++;
            }
        }

        public DeathCounts(int stageID)
        {
            this.StageID = stageID;
            this.deathes = new Dictionary<DeathType, int>();
            foreach (var type in Enum.GetValues(typeof(DeathType)))
            {
                this.deathes.Add((DeathType)type, 0);
            }
        }

        public int SumCount
            => this.deathes.Values.Sum();
        public bool IsDeathMinimize
            => this.MinDeath >= this.SumCount;
        public int MinDeath
            => GameManager.StageData.Get(this.StageID).HeiwaDeathBorder;
    }


    public class CoinScore
    {
        public bool IsGettedMedal { get; private set; }
        public int CoinCount { get; private set; }
        public int StageID { get; private set; }

        public CoinScore(int stageID)
        {
            this.StageID = stageID;
            this.IsGettedMedal = false;
            this.CoinCount = 0;
        }

        public void GetCoin()
        {
            this.CoinCount++;
            GameEventManager.CoinCountChangedEvent.Raise();
        }
        public void GetMedal()
        {
            this.IsGettedMedal = true;
            GameEventManager.CoinCountChangedEvent.Raise();
        }

        public string CoinStatus
            => String.Format("{0}/{1}", this.CoinCount, this.MaxCoinCount);
        public bool IsCoinGetAll
            => this.CoinCount >= this.MaxCoinCount;
        public int MaxCoinCount
            => GameManager.StageData.Get(this.StageID).MaxCoinAmount;

    }
}