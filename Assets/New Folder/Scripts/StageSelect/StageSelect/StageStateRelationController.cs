using UnityEngine;
using Game.Data;
using System.Collections;
using System.Collections.Generic;

namespace Game.StageSelect
{

    [RequireComponent(typeof(StageMass))]
    [RequireComponent(typeof(Mass))]

    public class StageStateRelationController : StageMassRelationController
    {

/*        public new List<StageMass> RelationMasses
            => this.SerchNextStageMasses(this.mass);
            */

        protected List<StageMass> SerchNextStageMasses(Mass mass)
        {
            var list = new List<StageMass>();
            foreach (var ma in this.mass.next)
            {
                var nextMass = ma.Value;
                if (nextMass != null)
                {
                    var s = nextMass.GetComponent<StageMass>();
                    if (s != null)
                    {
                        list.Add(s);
                    }
                    else
                    {
                        var l = this.SerchNextStageMasses(nextMass);
                        if (l.Count >= 1)
                        {
                            list.AddRange(l);
                        }
                    }
                }
            }
            return list;
        }

        protected override StageState ChkThisActivate()
        {
            this.RelationMasses = this.SerchNextStageMasses(this.mass);
            return base.ChkThisActivate();
        }

        /*
        protected override StageState ChkThisActivate()
        {
            StageInformation info = this.stageMass.StageInformation;
            
            if (info.IsCleared)
            {
                return StageState.cleared;
            }
            bool open = this.defaultOpened;
            foreach (var ma in this.mass.next)
            {
                var nextMass = ma.Value;
                if (nextMass != null)
                {
                    var s = nextMass.GetComponent<StageMass>();
                    if (s != null) {
                        if (s.StageInformation.IsCleared)
                        {
                            open = true;
                            break;
                        }
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
        */
    }
}