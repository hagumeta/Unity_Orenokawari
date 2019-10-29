using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using InputUtil;
using Extends.UI;
using Game.Stage.Manager;
//参考：https://qiita.com/calmbooks/items/5d78a60f07921238020e

namespace Game.Stage {
    public class StageClearDisplayer : SingletonMonoBehaviourFast<StageClearDisplayer>
    {
        public TextMeshProUGUI DeathCountText, CoinCountText;
        public Transform ClearTextObj, DeathCountTextObj, CoinCountTextObj, OreMedalObj;
        public MessageController OreComment;

        public delegate void onCompleteCallback();
        public static void Create(onCompleteCallback callback)
        {
            Instance.gameObject.SetActive(true);
            Instance.InitAction(callback);
        }


        private bool isActionEnded = false;
        private onCompleteCallback completeCallback;

        protected override void Init()
        {
            this.gameObject.SetActive(false);
            this.isActionEnded = false;
        }

        private void InitAction(onCompleteCallback callback)
        {
            this.DeathCountText.text = StageManager.MyDeathCounts.SumCount.ToString();
            this.CoinCountText.text = StageManager.MyCoinScore.CoinCount.ToString();
            this.OreMedalObj.transform.localScale = Vector2.zero;

            this.isActionEnded = false;
            this.completeCallback = callback;
            StartCoroutine(this.action());
        }

        private IEnumerator action()
        {
            this.ClearTextObj.DOLocalMoveX(0, 1f);

            yield return new WaitForSeconds(0.6f);

            this.OreComment.SetMessage(this.GetOreComment(), 0.05f);
            this.OreComment.transform.DOLocalMoveX(0, 0.5f);

            yield return new WaitForSeconds(0.4f);
            this.DeathCountTextObj.DOLocalMoveX(0, 0.5f);

            yield return new WaitForSeconds(0.2f);
            this.CoinCountTextObj.DOLocalMoveX(0, 0.5f);

            if (StageManager.MyCoinScore.IsGettedMedal)
            {
                yield return new WaitForSeconds(0.4f);
                this.OreMedalObj.DOScale(new Vector2(1f, 1f), 0.3f).SetEase(Ease.OutBounce);
            }

            yield return new WaitForSeconds(1f);
            this.isActionEnded = true;
            this.completeCallback();
        }




        private string oreComment_coinAll
            = "この世は金！！\nここら辺の金は全て俺が頂いた！！";
        private string oreComment_deathMin
            = "何という平和的クリア...！\nいい腕してるぜ、相棒！！";
        private string oreComment_normal
            = "他愛もないステージだったな．\n相棒、さっさと次にいこうぜ！！";
        private string oreComment_medalGet
            = "手に入れたぞ！俺メダル！！";
        private string oreComment_allTresureGet
            = "相棒、お前はコレクターか？\nおかげで俺は金持ちになりそうだぜ．\nありがとよ！";
        private string oreComment_perfect
            = "全て完璧だ...  お前は天才か...？\n\nクク...\nこれからも頼りにしてるぜ！！相棒！！";

        private string GetOreComment()
        {
            var coinGetAll = StageManager.MyCoinScore.IsCoinGetAll;
            var getMedal = StageManager.MyCoinScore.IsGettedMedal;
            var deathMin = StageManager.MyDeathCounts.IsDeathMinimize;
            if (coinGetAll && getMedal && deathMin) {
                return this.oreComment_perfect;
            }
            if (coinGetAll && getMedal){
                return this.oreComment_allTresureGet;
            }
            if (deathMin)
            {
                return this.oreComment_deathMin;
            }
            if (coinGetAll){
                return this.oreComment_coinAll;
            }
            if (getMedal){
                return this.oreComment_medalGet;
            }
            return this.oreComment_normal;
        }        
    }
}