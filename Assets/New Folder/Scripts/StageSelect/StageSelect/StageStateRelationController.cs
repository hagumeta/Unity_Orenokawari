using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Stage;


namespace Game.StageSelect
{

    [RequireComponent(typeof(StageMass))]
    public class StageStateRelationController : MonoBehaviour
    {

        protected StageMass stageMass
            => this.GetComponent<StageMass>();


        protected  StageState ChkThisActivate()
        {
            return StageState.locked;
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}