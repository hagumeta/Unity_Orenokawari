using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Stage;


namespace Game.StageSelect
{

    [RequireComponent(typeof(StageMass))]
    [RequireComponent(typeof(Mass))]

    public class StageStateRelationController : MonoBehaviour
    {
        [SerializeField] private bool defaultOpened = false;

        public StageMass stageMass
            => this.gameObject.GetComponent<StageMass>();
        protected Mass mass
            => this.gameObject.GetComponent<Mass>();

        protected StageState ChkThisActivate()
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

        void Start()
        {
        }

        void Update()
        {

        }
    }
}