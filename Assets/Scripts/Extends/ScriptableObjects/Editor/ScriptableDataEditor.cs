using UnityEditor;

namespace Extends.ScriptableDatas
{
    [CustomEditor(typeof(ScriptableData))]
    public class ScriptableDataEditor : Editor
    {
        protected ScriptableData scriptableData;
        private void OnEnable()
        {
            this.scriptableData = target as ScriptableData;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("ID : ", this.scriptableData.Id.ToString());
            base.OnInspectorGUI();
        }
    }

}
