using UnityEngine;
using UnityEditor;


namespace EditorExtends
{
    /// <summary>
    /// 参考：https://qiita.com/Gok/items/96e8747269bf4a2a9cc5
    /// </summary>

    public class EditorGUILayoutEx
    {
        /// <summary>
        /// インデントレベル設定を考慮した仕切り線.
        /// </summary>
        /// <param name="useIndentLevel">インデントレベルを考慮するか.</param>
        public static void Separator(bool useIndentLevel = false)
        {
            EditorGUILayout.BeginHorizontal();
            if (useIndentLevel)
            {
                GUILayout.Space(EditorGUI.indentLevel * 15);
            }
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// インデントレベルを設定する仕切り線.
        /// </summary>
        /// <param name="indentLevel">インデントレベル</param>
        public static void Separator(int indentLevel)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentLevel * 15);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
            EditorGUILayout.EndHorizontal();
        }
    }
}
