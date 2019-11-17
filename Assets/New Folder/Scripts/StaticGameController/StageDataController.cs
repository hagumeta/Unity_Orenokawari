using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data.Static;

namespace Game.Data
{

    public abstract class DataController<T> where T : Object
    {
        public List<T> DataList { private set; get; }

        /// <summary>
        /// データが保存されているディレクトリパスを入力してください
        /// </summary>
        public abstract string DataPath { get; }　
        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataController()
        {
            this.Load();
        }

        /// <summary>
        /// int型のIDを基に自身の中の要素を検索して返す
        /// 用途によって自由に拡張してください
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public abstract T Get(int resourceID);


        /// <summary>
        /// DataPathに保存されたデータ<T>をロード，DataListに格納する
        /// </summary>
        public void Load()
        {
            var load = new List<Object>(Resources.LoadAll(this.DataPath, typeof(T)));
            this.DataList = load.ConvertAll<T>(a => (T)a);

            if (this.DataList == null)
            {
                Debug.LogError(this.DataPath + " not found");
                this.DataList = new List<T>();
            }
        }
    }

    public class StageDataController : DataController<StageData>
    {
        public override string DataPath
            => "ScriptableDatas/StageDatas";

        public override StageData Get(int resourceID)
        {
            return this.DataList.Find(d => d.StageId == resourceID);
        }
    }

    public class WorldDataController : DataController<WorldData>
    {
        public override string DataPath
            => "ScriptableDatas/WorldDatas";

        public override WorldData Get(int resourceID)
        {
            return this.DataList.Find(d => d.WorldId == resourceID);
        }
    }
}