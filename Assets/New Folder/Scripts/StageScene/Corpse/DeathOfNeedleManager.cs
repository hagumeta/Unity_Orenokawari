using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Corpse
{
    public class DeathOfNeedleManager : DeathCorpseManager
    {
        [SerializeField] private GameObject[] CorpsePatturns;
        protected void Start()
        {
            int index = Mathf.FloorToInt(Random.Range(0, this.CorpsePatturns.Length));
            Instantiate(this.CorpsePatturns[index], this.transform);
        }
    }
}