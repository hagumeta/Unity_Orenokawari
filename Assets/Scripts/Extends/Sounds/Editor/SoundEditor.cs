using UnityEditor;
using Extends.ScriptableDatas;
using System;
using EditorExtends;

namespace Extends.Sounds
{
    [CustomEditor(typeof(Sound))]
    public class SoundEditor : ScriptableDataEditor
    {
        private static bool show;
        public static void ShowSoundData(Sound sound)
        {
            if (sound == null)
            {
                EditorGUILayout.BeginFoldoutHeaderGroup(show, "empty Sound");
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            show = EditorGUILayout.BeginFoldoutHeaderGroup(show, string.Format("{0} ({1})", sound.name, sound.GetType().Name));
            if (show)
            {
                EditorGUILayoutEx.Separator();
                Type t = sound.GetType();
                foreach (var param in t.GetFields())
                {
                    var val = param.GetValue(sound);
                    string label = null;
                    if (val != null) {
                        label = val.ToString();
                    }
                    EditorGUILayout.LabelField(param.Name, label);
                    EditorGUILayoutEx.Separator();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
