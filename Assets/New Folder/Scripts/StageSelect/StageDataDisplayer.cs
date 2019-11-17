using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Game.Data;

namespace Game.StageSelect
{
    public class StageDataDisplayer : MonoBehaviour
    {
        public TextMeshProUGUI stageNumberText, stageNameText, deathCountAllText;
        public DeathCountsDisplayer deathCountsDisplayer;
        public CoinCountDisplayer coinCountDisplayer;

        public void SetStageData(StageInformation info)
        {
            this.stageNumberText.text = info.StageNumber;
            this.stageNameText.text = info.StageName;

            this.deathCountsDisplayer.SetDeathsCounts(info.DeathCounts.deathes);
            this.coinCountDisplayer.SetCoinScore(info.CoinScore);
            this.deathCountAllText.text = info.SumDeathCount.ToString();
        }
    }
}