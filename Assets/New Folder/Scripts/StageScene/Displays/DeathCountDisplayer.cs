using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game;

namespace Game.Stage.Displayer
{
    public class DeathCountDisplayer : DeathCountChangedEventListener
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        private int _count;

        private int deathCount
        {
                set
            {
                if (this._count != value && this._text != null)
                {
                    this._text.text = value.ToString();
                }
            }
        }

        public override void OnDeathCountChanged()
        {
            this.deathCount = StageManager.MyDeathCounts.SumCount;
        }
    }
}