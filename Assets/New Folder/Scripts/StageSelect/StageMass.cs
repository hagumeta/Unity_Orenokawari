using UnityEngine;
using DG.Tweening;
using Game.Stage;
using InputUtil;
using TMPro;
using System.Linq;
using Extends.Cursor;
using System;
using Game.Data;

namespace Game.StageSelect
{
    [System.Serializable]
    public enum StageState
    {
        cleared,
        notCleared,
        locked
    }

    public class StageMass : SelectMass
    {
        [SerializeField]
        protected TextMeshPro stageNumberText;
        [SerializeField]
        public GameObject
            UnclearedStageIcon,
            LockedStageIcon,
            ClearedStageIcon;

        public StageState state;

        private Vector3 defaultScale;

        public string StageNum
        {
            get => this.stageNumberText.text;
            set => this.stageNumberText.text = value;
        }
        public StageInformation StageInformation
            => ((StageSelectController)this.controller).GetStageInformation(this.id);

        /// <summary>
        /// マス上に表示するステージ番号を設定する
        /// </summary>
        /// <param name="stageNum"></param>
        public void SetStageNumber(string stageNum)
        {
            this.StageNum = stageNum;
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
                    foreach (var a in (Extends.Cursor.Cursor[])GameObject.FindObjectsOfType(typeof(Extends.Cursor.Cursor)))
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
                var rel = this.GetComponent<StageStateRelationController>();
                if (rel != null)
                {
                    rel.SetStageState();
                }

                this.ClearedStageIcon.SetActive(false);
                this.UnclearedStageIcon.SetActive(false);
                this.LockedStageIcon.SetActive(false);
                this.stageNumberText.gameObject.SetActive(false);

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
                    default:
                        this.LockedStageIcon.SetActive(true);
                        break;
                }
            }
            catch (Exception e){
                Debug.Log(e);
                this.state = StageState.locked;
                this.ClearedStageIcon.SetActive(false);
                this.UnclearedStageIcon.SetActive(false);
                this.LockedStageIcon.SetActive(true);
                this.stageNumberText.gameObject.SetActive(false);
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