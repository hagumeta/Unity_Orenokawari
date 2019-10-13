using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Data.Stage
{
    [CreateAssetMenu(menuName = "StageDatas/Create StageData", fileName = "ParameterTable")]
    public class StageData : ScriptableObject
    {
        public int StageId
            => GetInstanceID();
        public SceneObject StageScene;
        public string StageName;
        public int MaxCoinAmount;
        public int HeiwaDeathBorder;
#if UNITY_EDITOR
        [MenuItem("Assets/Create/StageData")]
        static void CreateStageData()
        {
            var exampleAsset = CreateInstance<StageData>();

            AssetDatabase.CreateAsset(exampleAsset, "Assets/StageData.asset");
            AssetDatabase.Refresh();
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(StageData))]
    public class StageDataEditor : Editor
    {
        StageData data;
        private void OnEnable()
        {
            data = target as StageData;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("ID : ", data.StageId.ToString());
            base.OnInspectorGUI();
        }
    }
#endif
}



/*
namespace Data.Stage {
    public class StageDatas : ScriptableObject
    {
        [System.Serializable]
        public class StageData
        {
            public int ID;
            [SerializeField]public SceneObject StageScene;
            [SerializeField]public string StageName;
            internal bool SoftDeleted;
        }

        [MenuItem("Assets/Create/StageDatas/StageDatas")]
        static void CreateExampleAsset()
        {
            var exampleAsset = CreateInstance<StageDatas>();

            AssetDatabase.CreateAsset(exampleAsset, "Assets/ExampleAsset.asset");
            AssetDatabase.Refresh();
        }

        [SerializeField]
        public List<StageData> StageDataList;
    }
}*/