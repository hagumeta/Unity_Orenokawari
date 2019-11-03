using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Corpse
{
    public class DeathOfNeedleManager : Corpse
    {
        [SerializeField] private GameObject[] corpsePatturns;
        [SerializeField] private Transform boodEmitter;

        protected override IEnumerator CorpseCoroutine()
        {
            this.boodEmitter.gameObject.SetActive(true);

            int index = Mathf.FloorToInt(Random.Range(0, this.corpsePatturns.Length));
            Instantiate(this.corpsePatturns[index], this.transform);

            yield return new WaitForSeconds(1f);
            if (!this.IgnoreNamusan) {
                this.Namusan();
            }
        }
    }
}