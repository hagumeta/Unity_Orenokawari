using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Stage.Displayer
{
    public class CoinCountDisplayer : CoinCountChangedEventListener
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        private string _count;

        private string coinCount
        {
            set
            {
                if (this._count != value && this._text != null)
                {
                    this._text.text = value.ToString();
                }
            }
        }

        public override void OnCoinCountChanged()
        {
            this.coinCount = StageManager.MyCoinScore.CoinStatus;
        }

        private void Start()
        {
            this.OnCoinCountChanged();
        }
    }
}