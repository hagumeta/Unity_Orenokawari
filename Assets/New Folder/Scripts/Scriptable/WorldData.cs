using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace Data.Stage
{
    [CreateAssetMenu(menuName = "StageDatas/Create WorldData", fileName = "ParameterTable")]
    public class WorldData : ScriptableObject
    {
        public string WorldName;

        [SerializeField]
        public ExtStageData[] StageDatas;
        public int WorldId
            => this.GetInstanceID();

        public int GetStageID(int index)
            => this.StageDatas[this.FixIndex(index)].stageData.StageId;
        public string getStageExtName(int index)
            => this.StageDatas[this.FixIndex(index)].extName;


        /// <summary>
        /// indexに応じたステージ番号を返す
        /// 〇-〇みたいな形式
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetStageNumber(int index)
        {
            if (index == -1)
            {
                return "-----";
            }
            else
            {
                return this.WorldName + "-" + (++index);
            }
        }

        /// <summary>
        /// このWorldDataからStageDataを探索してそのIndexを返す．
        /// 無ければ-1を返す
        /// </summary>
        /// <param name="stageID"></param>
        /// <returns></returns>
        public int GetStageIndex(int stageID)
        {
            int index = 0;
            for (; index < this.StageDatas.Length; index++)
            {
                if (this.GetStageID(index) == stageID)
                {
                    break;
                }
            }
            if (index < this.StageDatas.Length)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }



        public int FixIndex(int index)
        {
            if (index < 0)
            {
                return 0;
            }
            else if (index > this.StageDatas.Length-1)
            {
                return this.StageDatas.Length - 1;
            }
            else
            {
                return index;
            }
        }


        [System.Serializable]
        public class ExtStageData
        {
            public string extName;
            public StageData stageData;
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(WorldData))]
    public class WorldDataEditor : Editor
    {
        WorldData data;
        private void OnEnable()
        {
            data = target as WorldData;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("ID : ", data.WorldId.ToString());
            base.OnInspectorGUI();
        }
    }
#endif


    /*
        public class StageDataProp : PropertyAttribute
        {
            //属性の引数はここに書くが今回は引数なし
            public StageDataProp()
            {
            }
        }

        [CustomPropertyDrawer(typeof(StageDataProp))]
        public class WorldDataEditor : PropertyDrawer
        {
            private StageDataProp itemIdAttribute
            {
                get
                {
                    return (StageDataProp)attribute;
                }
            }
            private bool isInitialized = false;
            private string[] itemLabels = null;
            private StageData[] datas = null;


            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                //初期化
                if (!isInitialized)
                {
                    Dictionary<string, StageData> items = GetItemModelLabels();
                    itemLabels = new string[items.Count];
                    Debug.Log(items);
                    items.Keys.CopyTo(itemLabels, 0);
                    items.Values.CopyTo(datas, 0);

                    isInitialized = true;
                }


                int index = -1;
                for (int i=0; i<this.datas.Length; i++)
                {
                    if(this.datas[i] == property.objectReferenceValue)
                    {
                        index = i;
                        break;
                    }
                }

                property.objectReferenceValue = datas[EditorGUI.Popup(position, label.text, index, itemLabels, GUIStyle.none)];
            }

            public static Dictionary<string, StageData> GetItemModelLabels()
            {
                Dictionary<string, StageData> result = new Dictionary<string, StageData>();

                string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", "StageData"));
                if (guids.Length == 0)
                {
                    return result;
                }
                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    StageData data = AssetDatabase.LoadAssetAtPath<StageData>(assetPath);


                    //if (Regex.IsMatch(assetPath, "Assets/ScriptableDatas/StageDatas/*.asset"))
                    {
                        result[data.StageName] = data;
                    }
                }
                return result;
            }
        }
        */
}