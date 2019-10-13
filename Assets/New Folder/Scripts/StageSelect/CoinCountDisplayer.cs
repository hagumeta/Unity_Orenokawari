using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.StageSelect
{
    public class CoinCountDisplayer : MonoBehaviour
    {
        private CoinScore _coinScore;
        [SerializeField] private TextMeshProUGUI count_text;
        [SerializeField] private Image medalImage;

        private void ResetDisplay()
        {
            this.count_text.text = this._coinScore.CoinStatus;
            this.count_text.color = this._coinScore.IsCoinGetAll ? Color.red : Color.black;

            if (this._coinScore.IsCoinGetAll)
            {
                this.count_text.color = Color.red;
            }
            if (this._coinScore.IsGettedMedal)
            {
                this.medalImage.color = new Color(1f, 1f, 1f, 1f);
            } else {
                this.medalImage.color = new Color(0f, 0f, 0f, 0.5f);
            }
        }

        public void SetCoinScore(CoinScore coinScore)
        {
            this._coinScore = coinScore;
            this.ResetDisplay();
        }
    }
}