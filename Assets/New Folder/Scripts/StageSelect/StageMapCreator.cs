using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Stage;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.StageSelect
{
    [RequireComponent(typeof(StageSelectController))]
    public class StageMapCreator : MassMapCreator
    {
        protected new StageMass[] Masses
            => this.GetComponentsInChildren<StageMass>();

        public override void MapCreate()
        {
            base.MapCreate();
            for (int j = 0; j < this.Masses.GetLength(0); j++)
            {
                this.Masses[j].id = j;
                this.Masses[j].StageNum = (j + 1).ToString();
            }
        }
        private void Start()
        {
            Invoke("SetUpStageDatas", Time.deltaTime);
        }
        protected void SetUpStageDatas()
        {
            var controller = this.GetComponent<StageSelectController>();
            var worldData = controller.WorldData;

            int c = 0;
            int max = worldData.StageDatas.Length;
            for (int j = 0; j < this.Masses.GetLength(0); j++)
            {
                var info = controller.GetStageInformation(c);

                var mass = this.Masses[j];
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

#if UNITY_EDITOR
    [CustomEditor(typeof(StageMapCreator))]
    public class StageMapCreatorEditor : BaseCustomMapEditor<StageMapCreator>
    {

    }
#endif
}