using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Stage;
using UnityEngine.SceneManagement;
using Game.Sounds;
using System.Linq;
using Extends.Cursor;

namespace Game.StageSelect{
    public class StageSelectController : MonoBehaviour, ISelectController
    {
        [SerializeField]
        private StageInformation[] stageInformationList;
        [SerializeField]
        private StageDataDisplayer stageDataDisplayer;
        [SerializeField]
        private WorldData worldData;
        [SerializeField]
        private Cursor_forMassEvent mainCursor;

        public Information[] InformationList
            => this.stageInformationList as StageInformation[];



        private void Start()
        {
            this.LoadData(this.worldData);
            Invoke("SetCursorPosition", 0.5f);
        }

        private void SetCursorPosition()
        {
            try
            {
                var beforeInfo = GameManager.NowStageInformation;
                var index = this.worldData.GetStageIndex(beforeInfo.StageInformation.StageID);
                var list = this.GetComponentsInChildren<StageMass>();
                var mass = list.Where(m => m.id == index).Single();
                this.mainCursor.MoveToMass(mass, true);
            }
            catch
            {
                var list = this.GetComponentsInChildren<StageMass>();
                this.mainCursor.MoveToMass(list.FindMin(a => a.id), true);
            }
        }


        /// <summary>
        /// indexから対応するStageInformationを取得する
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public StageInformation GetStageInformation(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            else if (index >= this.stageInformationList.Length)
            {
                index = this.stageInformationList.Length - 1;
            }

            return this.stageInformationList[index];
        }


        /// <summary>
        /// 決定ボタンが押された際のアクション
        /// リクエストを生成してステージ移動をする
        /// </summary>
        /// <param name="index"></param>
        public void InvokeEnteredAction(int index)
        {
            var request = this.CreateMoveStageRequest(index);
            GameManager.MoveStageScene(request);
            GlobalAudioPlayer.PlayAudio(GlobalAudioCollection.SESetting.MenuEnterSound);
        }

        /// <summary>
        /// カーソルが移動した場合のアクション
        /// Displayerに現在のカーソルに対応した情報を与える
        /// </summary>
        /// <param name="index"></param>
        public void InvokeSelectedAction(int index)
        {
            this.stageDataDisplayer.SetStageData(this.GetStageInformation(index));
            GlobalAudioPlayer.PlayAudio(GlobalAudioCollection.SESetting.MenuSelectSound);
        }


        /// <summary>
        /// WorldDataのデータを読み込み，StageInfomationのリストを用意する
        /// </summary>
        /// <param name="worldData"></param>
        public void LoadData(WorldData worldData)
        {
            List<StageInformation> infoList = new List<StageInformation>();
            for (int i=0; i<worldData.StageDatas.Length; i++)
            {
                infoList.Add(new StageInformation(worldData.WorldId, i));
            }

            this.stageInformationList = infoList.ToArray();
        }


        /// <summary>
        /// indexに対応したステージへ移動するためのリクエストを生成する
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private MoveStageRequest CreateMoveStageRequest(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            else if (index >= this.stageInformationList.Length)
            {
                index = this.stageInformationList.Length - 1;
            }
            var info = this.stageInformationList[index];
            var nowScene = SceneManager.GetActiveScene();
            return new MoveStageRequest(info, nowScene);
        }


        public Information GetInformation(int index) { return null; }
    }
}