using UnityEngine;
using DG.Tweening;
using Data.Stage;
using InputUtil;
using TMPro;
using System.Linq;

namespace Game.StageSelect
{

    public class StageMass : SelectMass
    {
        [SerializeField]
        protected TextMeshPro stageNumberText;
        [SerializeField]
        public GameObject
            UnclearedStageIcon,
            LockedStageIcon,
            ClearedStageIcon;

        public string massName;
        public StageState state;

        private Vector3 defaultScale;

        /// <summary>
        /// マス上に表示するステージ番号を設定する
        /// </summary>
        /// <param name="stageNum"></param>
        public void SetStageNumber(string stageNum, StageState state)
        {
            if (this.stageNumberText != null)
            {
                this.stageNumberText.text = stageNum;
            }
            this.massName = stageNum;
            this.state = state;

            this.ResetState();
        }

        void Start()
        {
            this.defaultScale = this.transform.localScale;
        }



        protected override void actionOnSelected()
        {
            if (this.state != StageState.locked)
            {
                if (ButtonOperation.Submit.IsPressed)
                {
                    foreach (var a in (Cursor[])GameObject.FindObjectsOfType(typeof(Cursor)))
                    {
                        a.isActivate = false;
                    }
                    this.EnterSelect();
                }
            }
        }



        /// <summary>
        /// ステージ状態からマスの状態を更新する
        /// </summary>
        public void ResetState()
        {
            try
            {
                switch (this.state)
                {
                    case StageState.notCleared:
                        this.stageNumberText.gameObject.SetActive(true);
                        this.UnclearedStageIcon.SetActive(true);
                        break;
                    case StageState.cleared:
                        this.stageNumberText.gameObject.SetActive(true);
                        this.ClearedStageIcon.SetActive(true);
                        break;
                    case StageState.locked:
                        this.stageNumberText.gameObject.SetActive(false);
                        this.LockedStageIcon.SetActive(true);
                        break;
                }
            }
            catch {
                this.state = StageState.locked;
            }

            this.GetComponent<Mass>().IsActive = !(this.state == StageState.locked);
            
        }



        /// <summary>
        /// このマス上にカーソルが乗った時にコールされるイベント
        /// </summary>
        protected override void actionSelected()
        {
            base.actionSelected();
            if (DOTween.IsTweening(this.transform))
            {
                DOTween.Kill(this.transform);
            }
            this.transform.DOScale(this.defaultScale * 1.3f, 0.4f);
        }

        /// <summary>
        /// このマス上からカーソルが離れた時にコールされるイベント
        /// </summary>
        protected override void actionUnSelected()
        {
            if (DOTween.IsTweening(this.transform))
            {
                DOTween.Kill(this.transform);
            }
            this.transform.DOScale(this.defaultScale, 0.2f);
        }
    }
}