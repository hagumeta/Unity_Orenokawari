using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game.StageSelect
{
    public class StageMassRelationController : MonoBehaviour
    {
        [SerializeField] protected bool defaultOpened = false;
        public List<StageMass> RelationMasses;

        public StageMass stageMass
            => this.gameObject.GetComponent<StageMass>();
        protected Mass mass
            => this.gameObject.GetComponent<Mass>();


        protected virtual StageState ChkThisActivate()
        {
            StageInformation info = this.stageMass.StageInformation;

            if (info.IsCleared)
            {
                return StageState.cleared;
            }
            bool open = this.defaultOpened;
            foreach (var mass in this.RelationMasses)
            {
                if (mass != null)
                {
                    if (mass.StageInformation.IsCleared)
                    {
                        open = true;
                        break;
                    }
                }
            }
            if (open || info.IsOpened)
            {
                return StageState.notCleared;
            }
            else
            {
                return StageState.locked;
            }
        }

        public void SetStageState()
        {
            this.stageMass.state = this.ChkThisActivate();
        }
    }
}