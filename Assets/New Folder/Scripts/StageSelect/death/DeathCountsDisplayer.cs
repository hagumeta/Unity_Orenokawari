using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Game.StageSelect
{
    public class DeathCountsDisplayer : MonoBehaviour
    {
        private List<DeathCountDisplayer> _counts;
        public DeathCountDisplayer _deathCountDisplayer;


        private void Start()
        {
            this._counts = new List<DeathCountDisplayer>();    
        }

        private void ResetDisplay()
        {
            foreach (var a in this._counts)
            {
                a.gameObject.SetActive(false);
            }
        }


        private void SetCount(Sprite deathIcon, int count)
        {
            var c = this._counts.FirstOrDefault(t => t.Icon == deathIcon);
            if (c != null)
            {
                c.gameObject.SetActive(true);
                c.Count = count;
            }
            else
            {
                var a = Instantiate(this._deathCountDisplayer.gameObject, this.transform);
                a.SetActive(true);
                var death = a.GetComponent<DeathCountDisplayer>();
                death.Icon = deathIcon;
                death.Count = count;
                this._counts.Add(death);
            }
        }


        public void SetDeathsCounts(Dictionary<DeathType, int> deathList)
        {
            this.ResetDisplay();

            foreach (var a in deathList)
            {
                
                var howdeath = GameManager.DeathIconCollection.deaths
                    .Where(h => h.deathType == a.Key)
                    .FirstOrDefault();
                
                if (howdeath != null)
                {
                    if (a.Value > 0 && howdeath.imageIcon != null)
                    {
                        this.SetCount(howdeath.imageIcon, a.Value);
                    }
                }

            }
        }
    }
}