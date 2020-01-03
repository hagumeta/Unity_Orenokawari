using UnityEditor;
using Extends.ScriptableDatas;
using System;

namespace Extends.Sounds
{
    [CustomEditor(typeof(Sound))]
    public class SoundEditor : ScriptableDataEditor
    {
        private static bool show;
        public static void ShowSoundData(Sound sound)
        {
            if (sound == null) return;

            show = EditorGUILayout.BeginFoldoutHeaderGroup(show, string.Format("{0} ({1})", sound.name, sound.GetType().Name));
            if (show)
            {
                Type t = sound.GetType();
                foreach (var param in t.GetFields())
                {
                    var val = param.GetValue(sound);
                    string label = null;
                    if (val != null) {
                        label = val.ToString();
                    }
                    EditorGUILayout.LabelField(param.Name, label);
                }
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
