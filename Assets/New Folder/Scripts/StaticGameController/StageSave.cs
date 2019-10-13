using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Game;

namespace Data.Stage
{
    //https://moon-bear.com/2019/03/23/%E3%80%90unity%E3%80%91json%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%9F%E3%82%BB%E3%83%BC%E3%83%96%E3%83%BB%E3%83%AD%E3%83%BC%E3%83%89%E5%87%A6%E7%90%86/
    //http://yasuand.hatenablog.com/entry/2013/09/12/051655

    [System.Serializable]
    public enum StageState
    {
        cleared,
        notCleared,
        locked
    }

    [System.Serializable]
    public class StageSave
    {
        public int stageID { private set; get; }
        public StageState state { private set; get; }
        public DeathCounts deathCounts { private set; get; }
        public CoinScore coinScore { private set; get; }
        public DateTime LastCleared{ private set; get; }
        

        public StageSave() { }
        public StageSave(int stageID)
        {
            this.stageID = stageID;
            this.state = StageState.notCleared;
            this.deathCounts = new DeathCounts(stageID);
            this.coinScore = new CoinScore(stageID);
        }

        public void SaveWithCleared(DeathCounts deathCounts, CoinScore coinScore=null)
        {
            this.deathCounts = deathCounts;
            this.LastCleared = DateTime.Now;
            this.state = StageState.cleared;
            this.coinScore = coinScore;
        }

        public void ChangeState(StageState state)
        {
            this.state = state;
        }
    }


    public class StageSaveDatas
    {
        public List<StageSave> stageResults;

        string key = "StageSaveDatas";

        public StageSaveDatas()
        {
            this.Load();
        }

        public void Load()
        {
            this.stageResults = new List<StageSave>();
            /*
            try
            {
                
                if (SaveData.Keys().Exists(key => key.Contains(this.key))) {
                    foreach (var key in SaveData.Keys().Where<string>(key => key.Contains(this.key)))
                    {
                        this.stageResults.Add(SaveData.GetClass<StageSave>(key, new StageSave(key)));
                    }
                }
                Debug.Log("ロードしたよ");
            }
            catch (Exception e)
            {
                Debug.Log("ロードしっぱい");
                Debug.Log(e);
            }*/
        }

        public void Save()
        {
            Debug.Log(this.stageResults.Count);
            foreach (var a in this.stageResults) {
                SaveData.SetClass<StageSave>(this.key + a.stageID, a);

                //SaveData.SetClass<DeathCounts>("json", a.deathCounts);
            }
            SaveData.Save();
            Debug.Log("セーブしたよ");
        }

        public StageSave Get(int stageID)
        {
            try
            {
                var ret = this.stageResults
                    .Where<StageSave>(a => a.stageID == stageID)
                    .First<StageSave>();
                return ret;
            }
            catch(Exception e)
            {
                var a = new StageSave(stageID);
                this.stageResults.Add(a);
                return a;
            }
        }

        public void Commit(StageSave stageSave)
        {
            var save = this.stageResults.Find(s => s.stageID == stageSave.stageID);
            if (save != null) {
                this.stageResults.Remove(save);
            }
            this.stageResults.Add(stageSave);
        }
    }
}