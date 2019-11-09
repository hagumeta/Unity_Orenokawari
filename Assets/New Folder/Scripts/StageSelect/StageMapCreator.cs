using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Stage;
using System.Linq;
using Extends.Cursor;

namespace Game.StageSelect
{
    [RequireComponent(typeof(StageSelectController))]
    public class StageMapCreator : MassMapCreator
    {
        [SerializeField]
        protected Cursor_forMassEvent cursor;

        [SerializeField]
        protected WorldData worldData;

        public override void MapCreate()
        {
            base.MapCreate();

            var controller = this.GetComponent<StageSelectController>();

            int c = 0;
            int max = this.worldData.StageDatas.Length;
            for (int i = this.Masses.GetLength(1) - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.Masses.GetLength(0); j++)
                {

                    var info = controller.GetStageInformation(c);

                    var mass = this.Masses[j, i].GetComponent<StageMass>();
                    mass.controller = controller;
                    mass.id = c;
                    mass.SetStageNumber(info.StageNumber, info.StageState);
                    mass.gameObject.SetActive(true);

                    if (c < max)
                    {
                        c++;
                    }
                }
            }
        }

        private void Start()
        {
            Invoke("MapCreate", Time.deltaTime);
        }
    }
}