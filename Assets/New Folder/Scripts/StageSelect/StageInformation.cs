using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage;
using Game.Data.Dynamic;
using Game.Data.Static;
using Extends.SelectUtils;

namespace Game.Data
{
    public class StageInformation : Information
    {
        public int WorldID { get; private set; }
        public int StageIndex { get; private set; }

        public StageSave StageSaveData { get; private set; }
        public DeathCounts DeathCounts
            => this.StageSaveData.deathCounts;
        public bool IsCleared
            => this.StageSaveData.stageStatus.isCleared;
        public bool IsOpened
            => this.StageSaveData.stageStatus.isOpened;

/*        public StageState StageState
            => this.StageSaveData.state;*/
        public DateTime LastCleared
            => this.StageSaveData.LastCleared;

        public StageData StageData
            => GameManager.StageData.Get(this.StageID);
        public WorldData WorldData
            => GameManager.WorldData.Get(this.WorldID);
        public string StageName
            => this.StageData.StageName;
        public SceneObject StageScene
            => this.StageData.StageScene;
        public string StageNumber
            => this.WorldData.GetStageNumber(this.StageIndex);
        public int StageID
            => this.WorldData.GetStageID(this.StageIndex);
        public int SumDeathCount
            => this.DeathCounts.SumCount;
        public CoinScore CoinScore
            => this.StageSaveData.coinScore;

        public StageInformation(int worldID, int stageIndex)
        {
            try
            {
                this.WorldID = worldID;
                this.StageIndex = this.WorldData.FixIndex(stageIndex);

                var saveData = GameManager.StageSaveData.Get(this.StageID);
                if (saveData == null)
                {
                    saveData = new StageSave(this.StageID);
                    GameManager.StageSaveData.Commit(saveData);
                }
                this.StageSaveData = saveData;
            }
            catch
            {
                Debug.Log("パースエラー : In StageInformation. \n" +
                    "worldID : " + worldID.ToString() + "   stageIndex : " + stageIndex);
            }
        }
    }


    public static class CoinScoreExtends
    {
        public static int MaxCoinCount(this CoinScore coinScore)
        {
            return GameManager.StageData.Get(coinScore.StageID).MaxCoinAmount;
        }
        public static bool IsCoinGetAll(this CoinScore coinScore)
        {
            return coinScore.CoinCount >= coinScore.MaxCoinCount();
        }
        public static string CoinStatus(this CoinScore coinScore)
        {
            return String.Format("{0}/{1}", coinScore.CoinCount, coinScore.MaxCoinCount());
        }
    }

    public static class DeathCountsExtends
    {
        public static int MinDeath(this DeathCounts deathCounts)
        {
            return GameManager.StageData.Get(deathCounts.StageID).HeiwaDeathBorder;
        }
        public static bool IsDeathMinimize(this DeathCounts deathCounts)
        {
            return deathCounts.MinDeath() >= deathCounts.SumCount;
        }
    }

}